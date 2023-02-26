namespace Authorization_Authentication.Models;

public class Permission
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<RolePermissionMap> RolePermissionMap { get; set; }
}

/*
 * one user can have more than 1 role
 * 1 role can have more than 1 permission
 * 
 */