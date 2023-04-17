namespace Authorization_Authentication.Application.Superadmin.Commands;

using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Shared.Application.Contracts;
using Shared.Application.CustomValidators;
using Shared.Core.Domain;
using Shared.Core.Exceptions;

public class AssignPermissionToRoleCommand:ICommand<Unit>
{

    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}

public class AssignPermissionToRoleValidator:AbstractValidator<AssignPermissionToRoleCommand>
{
    public AssignPermissionToRoleValidator()
    {
        RuleFor(command => command.PermissionId).SetValidator(new GuidValidator(nameof(AssignPermissionToRoleCommand.PermissionId)));
        RuleFor(command => command.RoleId).SetValidator(new GuidValidator(nameof(AssignPermissionToRoleCommand.RoleId)));
    }
}

internal class AssignPermissionToRoleCommandHandler:CommandHandlerBase<AssignPermissionToRoleCommand, Unit>
{
    private readonly AuthDbContext _db;

    public AssignPermissionToRoleCommandHandler(IErrorMessageCollector errorMessageCollector, AuthDbContext _db):base(errorMessageCollector)
    {
        this._db = _db;
    }

    public override async Task<Unit> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        /*RolePermission? rolePermissionMap =
            await _db.RolePermissionMaps.FirstOrDefaultAsync(x => x.PermissionId == request.PermissionId && x.RoleId == request.RoleId);
            */

        /*if (rolePermissionMap is not null) throw new BusinessLogicException("Permission already assigned to the role");*/

        Role? role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);

        if (role is null) throw new ArgumentNullException($"Role is not found for given  {request.RoleId}");

      //  Permission? permission = await _db.Permissions.FirstOrDefaultAsync(x => x.Id == request.PermissionId);

        //if (permission is null) throw new ArgumentNullException($"Permission is not found for given  {request.PermissionId}");

        /*try
        {
            await _db.RolePermissionMaps.AddAsync(new RolePermission
            {
                Role = role,
                Permission = permission
            });

            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occured in databa while adding permission to role", e);
        }*/


        return Unit.Value;
    }

}