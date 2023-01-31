namespace Customer.Test;

using Core.ServiceExtensions;

public class PhoneNumberTest
{
    [Fact]
    public void validatecode()
    {
        Service.ValidateCountryCode("+90");
    }
}