namespace Authorization_Authentication.Infrastructure.JWT;

public class JwtUserDto
{


    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
    public List<string> Permissions { get; set; } = new List<string>();

    public JwtToken Token { get; set; }
}