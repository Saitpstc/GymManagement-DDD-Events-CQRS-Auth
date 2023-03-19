namespace Authorization_Authentication.Models;

using Modules.Shared;

public class Permission
{
    public Guid Id { get; set; }
    public PermissionType Type { get; set; }
    public PermissionContext Context { get; set; }
    public ICollection<RolePermissionMap> RolePermissionMap { get; set; }
}

/*
 * one user can have more than 1 role
 * 1 role can have more than 1 permission
 * 
 */