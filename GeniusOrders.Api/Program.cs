using FluentValidation;
using GeniusOrders.Api.Behaviors;
using GeniusOrders.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// 1- add DbContext

builder.Services.AddDbContext<GeniusDbContext>(options =>
                    options.UseSqlite(builder.Configuration.GetConnectionString("GeniusConnection"))
);

//2 Add MedaitR
builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(typeof(Program).Assembly));

//3- Add Validator
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// 4- Add AutoMapper

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

builder.Logging.AddFilter("LuckyPennySoftware.MediatR.License", LogLevel.None);


var app = builder.Build();

app.MapControllers();

app.Run();
