namespace GymManagement.API.Controllers.Auth.v1;

using Authorization_Authentication.Application.Contracts;
using Authorization_Authentication.Application.Superadmin.Commands;
using Authorization_Authentication.Dto;
using Authorization_Authentication.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Modules.Shared;
using Shared.Application;
using Shared.Application.Contracts;
using Shared.Core;
using Shared.Infrastructure;
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
    

    [HttpPost("AssignPermissionToRole")]
    public async Task<ApiResponse<Unit>> AssignPermissionToRole(AssignPermissionToRoleCommand assignPermission)
    {
        Unit result = await _module.ExecuteCommandAsync(assignPermission);

        return CreateResponse(result);
    }

    [AuthorizeFilter(Permissions.CreateCustomer)]
    [HttpGet("Context")]
    public Task<ApiResponse<List<EnumResponse>>> GetContextForPermission()
    {

        var result = EnumExtensions.CreateEnumResponseList<PermissionContext>();

        return Task.FromResult(CreateResponse(result));
    }

    [HttpGet("PermissionType")]
    public Task<ApiResponse<List<EnumResponse>>> GetTypesOfPermissions()
    {
        var result = EnumExtensions.CreateEnumResponseList<PermissionType>();

        return Task.FromResult(CreateResponse(result));
    }
}