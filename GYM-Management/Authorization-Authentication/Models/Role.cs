namespace Authorization_Authentication.Models;

using Microsoft.AspNetCore.Identity;

public class Role:IdentityRole<Guid>
{
    public ICollection<UserRoleMap> UserRoles { get; set; }
}