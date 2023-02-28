namespace GymManagement.API.Controllers.Auth.v1;

using Authorization_Authentication.Application.Contracts;
using Authorization_Authentication.Application.Superadmin.Commands;
using Authorization_Authentication.Dto;
using GymManagement.API.Models;
using MediatR;
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
    public async Task<ApiResponse<RoleResponseDto>> CreateRole(CreateRoleCommand createRoleCommand)
    {
        var result = await _module.ExecuteCommandAsync(createRoleCommand);

        var results=CreateResponse(result);
        return results;
    }
    [HttpPost("Permission")]
    public async Task<ApiResponse<PermissionResponseDto>> CreatePermission(CreatePermissionCommand createPermission)
    {
        var result = await _module.ExecuteCommandAsync(createPermission);

        var results=CreateResponse(result);
        return results;
    }
    [HttpPost("AssignPermissionToRole")]
    public async Task<ApiResponse<Unit>> AssignPermissionToRole(AssignPermissionToRoleCommand assignPermission)
    {
        var result = await _module.ExecuteCommandAsync(assignPermission);

        return CreateResponse(result);
    }
}