namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;
using global::Customer.Application.Customer.Commands;
using Microsoft.AspNetCore.Mvc;

[Route("/customer")]
[ApiController]
public class CustomerController
{
    private readonly ICustomerModule _module;

    public CustomerController(ICustomerModule module)
    {
        _module = module;

    }

    [HttpPost]
    public async Task<ActionResult> test()
    {

        var resul =await _module.ExecuteCommandAsync(new CreateCustomer.Command(",", ",", ",", ",", ","));

        return new AcceptedResult();
    }
}