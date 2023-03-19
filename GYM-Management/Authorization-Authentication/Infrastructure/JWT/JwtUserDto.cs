namespace Authorization_Authentication.Infrastructure.JWT;

using Authorization_Authentication.Models;

public class JwtUserDto
{
    public JwtUserDto()
    {

    }

    public JwtUserDto(User user, JwtToken JwtToken)
    {
        Id = user.Id;
        UserName = user.UserName;
        Email = user.Email;
        Token = JwtToken;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string>? Roles { get; set; }
    public List<string>? Permissions { get; set; }

    public JwtToken Token { get; set; }
}