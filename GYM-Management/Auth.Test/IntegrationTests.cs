namespace Auth.Test;

using Microsoft.AspNetCore.Mvc.Testing;

public class IntegrationTests:TestBase
{

    private HttpClient _Client;

    public IntegrationTests()
    {
        _Client = new WebApplicationFactory<Program>().CreateDefaultClient();
    }

    [Fact]
    public void testes()
    {
        /*var createUserCommand=new CreateUserCommand()
        {
            Email = "saitpostaci8@gmail.com"
        }
        
        var responseBody = result.Content.ReadAsStringAsync();*/
    }
}