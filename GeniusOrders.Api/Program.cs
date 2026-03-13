using System.Runtime.InteropServices;
using System.Text;
using FluentValidation;
using GeniusOrders.Api.Behaviors;
using GeniusOrders.Api.Data;
using GeniusOrders.Api.Data.Services;
using GeniusOrders.Api.Entities;
using GeniusOrders.Api.Features.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
// 1- add DbContext

builder.Services.AddDbContext<GeniusDbContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("GeniusConnection"))
);



//2 Add MedaitR
builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<ITokenService, TokenService>();
//3- Add Validator
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// 4- Add AutoMapper

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);
#region IDentityUSer
//1- Add Identity
builder.Services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<GeniusDbContext>()
        .AddDefaultTokenProviders();
//2- Add Jwt Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = false;
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:key"]!))

    };
});


#endregion
var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    if (!await userManager.Users.AnyAsync())
    {
        var adminUser = new User
        {
            UserName = "admin",
            FullName = "Admin User",


        };
        var result = await userManager.CreateAsync(adminUser, "Admin@123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    ;
}

app.Run();
