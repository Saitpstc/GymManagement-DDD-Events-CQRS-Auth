namespace Authorization_Authentication.Models;

using Microsoft.AspNetCore.Identity;

public class Role:IdentityRole<Guid>
{
    public bool IsActive { get; set; }
    public ICollection<UserRoleMap> UserRoles { get; set; }
    public ICollection<RolePermission> RolePermissionMaps { get; set; }
}