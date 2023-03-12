namespace Authorization_Authentication.Application.Superadmin.Commands;

using FluentValidation;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Shared.Application.Contracts;
using Shared.Application.CustomValidators;

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

public class AssignPermissionToRoleCommandHandler:CommandHandlerBase<AssignPermissionToRoleCommand, Unit>
{
    private readonly AuthDbContext _db;

    public AssignPermissionToRoleCommandHandler(IErrorMessageCollector errorMessageCollector, AuthDbContext _db):base(errorMessageCollector)
    {
        this._db = _db;
    }

    public override async Task<Unit> Handle(AssignPermissionToRoleCommand request, CancellationToken cancellationToken)
    {
        RolePermissionMap? rolePermissionMap =
            await _db.RolePermissionMaps.FirstOrDefaultAsync(x => x.PermissionId == request.PermissionId && x.RoleId == request.RoleId);

        if (rolePermissionMap is not null)
        {
            ErrorMessageCollector.AddError("Permission already assigned to the role");
            return Unit.Value;
        }
        Role? role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);



        if (role is null)
        {
            ErrorMessageCollector.AddError("Role is not found");
            return Unit.Value;
        }

        Permission? permission = await _db.Permissions.FirstOrDefaultAsync(x => x.Id == request.PermissionId);

        if (permission is null)
        {
            ErrorMessageCollector.AddError("Permission is not found");
            return Unit.Value;
        }

        await _db.RolePermissionMaps.AddAsync(new RolePermissionMap
        {
            Role = role,
            Permission = permission
        });

        await _db.SaveChangesAsync();

        return Unit.Value;
    }
}