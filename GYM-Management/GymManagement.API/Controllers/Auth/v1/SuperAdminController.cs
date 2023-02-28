namespace GymManagement.API.Controllers.Auth.v1;

using Authorization_Authentication.Application.Contracts;
using Authorization_Authentication.Application.Superadmin.Commands;
using Authorization_Authentication.Dto;
using GymManagement.API.Models;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Contracts;

public class SuperAdminController:BaseController
{
    private readonly IAuthModule _module;

    public SuperAdminController(IErrorMessageCollector collector, IAuthModule module):base(collector)
    {
        _module = module;
    }

    [HttpPost("Role")]
    public async Task<ApiResponse<IDto>> CreateRole(CreateRoleCommand createRoleCommand)
    {
        var result = await _module.ExecuteCommandAsync(createRoleCommand);

        return CreateResponse(result);
    }
    [HttpPost("Permission")]
    public async Task<ApiResponse<IDto>> CreateUser(CreateRoleCommand createRoleCommand)
    {
        var result = await _module.ExecuteCommandAsync(createRoleCommand);

        return CreateResponse(result);
    }
}