namespace Ledger.Test;

public class TestBase
{
    public TestBase()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
    }
}