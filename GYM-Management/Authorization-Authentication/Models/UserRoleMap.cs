namespace Authorization_Authentication.Models;

using Microsoft.AspNetCore.Identity;

internal class UserRoleMap:IdentityUserRole<Guid>
{
    public virtual Role Role { get; set; }
    public virtual User User { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}