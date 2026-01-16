using Microsoft.AspNetCore.Identity;

namespace dewi.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; } = null!;
}