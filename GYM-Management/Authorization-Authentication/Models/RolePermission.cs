namespace Authorization_Authentication.Models;

using Modules.Shared;

internal class RolePermission
{
    public Guid RoleId { get; set; }

    public Role Role { get; set; }

    public PermissionType PermissionType { get; set; }
    public PermissionContext PermissionContext { get; set; }
}