namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;
using global::Customer.Application.Customer.Commands;
using global::Customer.Core;
using Microsoft.AspNetCore.Mvc;
using Models;
using Shared.Application.Contracts;

[Route("/customer")]

public class CustomerController:BaseController
{
    private readonly ICustomerModule _module;


    public CustomerController(ICustomerModule module, IErrorMessageCollector errorMessageCollector):base(errorMessageCollector)
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
    public async Task<ApiResponse<Customer>> Create(CreateCustomer.Command command)
    {
        var result = await _module.ExecuteCommandAsync(command);
        return CreateResponse(result);
    }
    
    
}

