namespace GymManagement.API.Controllers.Auth.v1;

using Authorization_Authentication.Application.Contracts;
using Authorization_Authentication.Application.Superadmin.Commands;
using Authorization_Authentication.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Contracts;
using Shared.Presentation.Models;

public class AdminController:BaseController
{
    private readonly IAuthModule _module;

    public AdminController(IErrorMessageCollector collector, IAuthModule module):base(collector)
    {
        _module = module;
    }

    [HttpPost("Role")]
    public async Task<ApiResponse<RoleResponseDto>> CreateRole(CreateRoleCommand createRoleCommand)
    {
        RoleResponseDto result = await _module.ExecuteCommandAsync(createRoleCommand);

        var results = CreateResponse(result);
        return results;
    }

    [HttpPost("Permission")]
    public async Task<ApiResponse<PermissionResponseDto>> CreatePermission(CreatePermissionCommand createPermission)
    {
        PermissionResponseDto result = await _module.ExecuteCommandAsync(createPermission);

        var results = CreateResponse(result);
        return results;
    }


    [HttpPost("AssignPermissionToRole")]
    public async Task<ApiResponse<Unit>> AssignPermissionToRole(AssignPermissionToRoleCommand assignPermission)
    {
        Unit result = await _module.ExecuteCommandAsync(assignPermission);

        return CreateResponse(result);
    }
}