namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

[Route("Customer/Customer")]
[ApiController]
public class CustomerController
{
    private readonly ICustomerModule _module;

    public CustomerController(ICustomerModule module)
    {
        _module = module;

    }

    [HttpPost]
    public async Task<ActionResult> Create()
    {
     // var result= await _module.ExecuteCommandAsync(new CreateCustomerCommand() );

      return new AcceptedResult();
    }
}