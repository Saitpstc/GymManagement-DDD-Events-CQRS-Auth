namespace GymManagement.API.Controllers.Customer;

using global::Customer.Application.Contracts;
using global::Customer.Application.Customer.Commands;
using global::Customer.Application.DTO.Response;
using global::Customer.Core;
using global::Customer.Core.Enums;
using global::Customer.Core.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Shared.Application;
using Shared.Application.Contracts;
using Shared.Infrastructure;
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
        var result = await _module.ExecuteCommandAsync(command);
        return CreateResponse(result);
    }
    [HttpPost("Membership")]
    public async Task<ApiResponse<MembershipStartedResponse>> Membership(StartMembershipCommand command)
    {
        var result = await _module.ExecuteCommandAsync(command);
        return CreateResponse(result);
    }

    [HttpGet("MembershipPeriods")]
    public Task<ApiResponse<List<EnumResponse>>> GetMembershipPeriods()
    {
        var response = EnumExtensions.CreateEnumResponseList<SubscriptionEnum>();

        return Task.FromResult(CreateResponse(response));
    }
}

