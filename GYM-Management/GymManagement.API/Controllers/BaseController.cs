namespace GymManagement.API.Controllers;

using global::Customer.Application.Contracts;

public class BaseController
{
    private readonly ICustomerModule _module;

    public BaseController(ICustomerModule module)
    {
        _module = module;

    }
}