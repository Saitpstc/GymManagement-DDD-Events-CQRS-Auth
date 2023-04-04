namespace Auth.Entry;

public interface IAuthService
{
    SharedUserModel GetCurrentUser();
}