namespace Authorization_Authentication.Dto;

using Models;

public class PermissionResponseDto
{

    public PermissionResponseDto(Permission permission)
    {
        Name = $"{permission.Context.ToString()}.{permission.Type.ToString()}";
        Id = permission.Id;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

}