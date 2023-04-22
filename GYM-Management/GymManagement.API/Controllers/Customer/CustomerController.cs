namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;
using global::Customer.Application.Customer.Commands;
using global::Customer.Application.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Contracts;
using Shared.Presentation.Models;

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
        return new ApiNavigation
        {
            Action = "POST",
            Operation = "Create Customer",
            EndPoint = "/customer",
            AcceptedData = new CreateCustomerCommand()
        };
    }

    [HttpPost]
    public async Task<ApiResponse<CustomerCreatedResponse>> Create(CreateCustomerCommand command)
    {
        CustomerCreatedResponse result = await _module.ExecuteCommandAsync(command);
        return CreateResponse(result);
    }

    [HttpPost("Membership")]
    public async Task<ApiResponse<MembershipStartedResponse>> Membership(StartMembershipCommand command)
    {
        MembershipStartedResponse result = await _module.ExecuteCommandAsync(command);
        return CreateResponse(result);
    }
}