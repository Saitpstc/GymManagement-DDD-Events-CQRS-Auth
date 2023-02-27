namespace Authorization_Authentication.Infrastructure.JwtToken;

public  class JwtUserDto
{
    public JwtUserDto(int id,string userName, string email)
    {
        Id = id is 0 or < 0 ? throw new ArgumentException("Id should be more than 0 and positive") : id;
        UserName = userName;
        Email = email;
    }

    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string>? Roles { get; set; }
    public List<string>? Permissions { get; set; }
}