namespace Auth.Entry;

public class SharedUserModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string UserId { get; set; }
    public string? StripeId { get; set; }

}