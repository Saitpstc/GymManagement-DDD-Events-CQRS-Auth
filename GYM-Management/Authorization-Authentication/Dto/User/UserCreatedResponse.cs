namespace Authorization_Authentication.Dto.User;

public class UserCreatedResponse
{

    public string UserName { get; set; }
    public string Email { get; set; }
    public Guid Id { get; set; }
}