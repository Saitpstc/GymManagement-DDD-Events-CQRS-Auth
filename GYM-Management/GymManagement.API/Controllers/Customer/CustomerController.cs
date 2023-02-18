namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;
using global::Customer.Application.Customer.Commands;
using Microsoft.AspNetCore.Mvc;
using Models;

[Route("/customer")]
[ApiController]
public class CustomerController:BaseController
{
    private readonly ICustomerModule _module;


    public CustomerController(ICustomerModule module)
    {
        _module = module;


    }

    [HttpGet("EntryPoint")]
    public async Task<ApiNavigation> EntryPoint()
    {
        return new ApiNavigation()
        {
            Action = "POST",
            Operation = "Create Customer",
            EndPoint = "/customer",
            AcceptedData = new CreateCustomer.Command()
        };
    }

    [HttpPost]
    public async Task<ApiResponse<CreateCustomerResponse>> Create(CreateCustomer.Command command)
    {

    }
}