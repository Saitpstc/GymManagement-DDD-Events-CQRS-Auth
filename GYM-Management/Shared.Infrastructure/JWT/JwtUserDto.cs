namespace Shared.Infrastructure.JWT;

public class JwtUserDto
{
    public JwtUserDto(Guid id, string userName, string email)
    {
        Id = id == Guid.Empty ? throw new ArgumentException("Id is not valid") : id;
        UserName = userName;
        Email = email;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string>? Roles { get; set; }
    public List<string>? Permissions { get; set; }

    public JwtToken Token { get; set; }
}