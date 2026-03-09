using Microsoft.AspNetCore.Identity;

namespace GeniusOrders.Api.Entities;


public class User : IdentityUser
{
    public string? FullName { get; set; }

}