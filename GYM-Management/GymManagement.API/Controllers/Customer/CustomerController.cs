namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;
using global::Customer.Application.Customer.Commands;
using Microsoft.AspNetCore.Mvc;
using Models;
using Shared.Application.Contracts;

[Route("/customer")]
[ApiController]
public class CustomerController:BaseController
{
    private readonly ICustomerModule _module;
    private readonly ErrorMessageCollector _errorMessageCollector;


    public CustomerController(ICustomerModule module, IErrorMessageCollector errorMessageCollector)
    {
        _module = module;
        _errorMessageCollector = (ErrorMessageCollector)errorMessageCollector;


    }

    [HttpGet("EntryPoint")]
    public async Task<ApiNavigation> EntryPoint()
    {
        var result = _module.ExecuteCommandAsync(new CreateCustomer.Command());

        var list=_errorMessageCollector.ErrorMessage;
        return new ApiNavigation()
        {
            Action = "POST",
            Operation = "Create Customer",
            EndPoint = "/customer",
            AcceptedData = new CreateCustomer.Command()
        };
    }

    /*[HttpPost]
    public async Task<ApiResponse<CreateCustomerResponse>> Create(CreateCustomer.Command command)
    {

    }*/
}

