namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;

using Microsoft.AspNetCore.Mvc;

[Route("/customer")]
[ApiController]
public class CustomerController
{
    private readonly ICustomerModule _module;
    private readonly ILogger<CustomerController> _logger;

    public CustomerController(ICustomerModule module, ILogger<CustomerController> logger)
    {
        _module = module;
        _logger = logger;

    }

    [HttpPost]
    public async Task<ActionResult> test()
    {
        await _module.ExecuteCommandAsync(new CreateCustomer.Command("name", "surname", "90", "1234567891", "test@gmail.com"));
        return new AcceptedResult();
    }
}