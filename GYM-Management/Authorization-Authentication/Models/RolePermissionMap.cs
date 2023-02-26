namespace Authorization_Authentication.Models;

public class RolePermissionMap
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }

}