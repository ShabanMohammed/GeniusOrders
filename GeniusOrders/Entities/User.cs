using Microsoft.AspNetCore.Identity;

namespace GeniusOrders.Entities;


public class User : IdentityUser
{
    public string FullName { get; set; } = default!;
    public string? Department { get; set; }

}