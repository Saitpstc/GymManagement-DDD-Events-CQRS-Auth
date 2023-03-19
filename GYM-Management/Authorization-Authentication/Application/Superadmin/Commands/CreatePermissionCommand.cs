namespace Authorization_Authentication.Application.Superadmin.Commands;

using Dto;
using FluentValidation;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Models;
using Shared.Application.Contracts;

public class CreatePermissionCommand:ICommand<PermissionResponseDto>
{

    public PermissionType PermissionType { get; set; }
}

public class CreatePermissionValidator:AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionValidator()
    {
        RuleFor(x => x.PermissionType).NotEmpty().NotNull();
    }
}

public class CreatePermissionCommandHandler:CommandHandlerBase<CreatePermissionCommand, PermissionResponseDto>
{
    private readonly AuthDbContext _context;


    public CreatePermissionCommandHandler(IErrorMessageCollector errorMessageCollector, AuthDbContext context):base(errorMessageCollector)
    {
        _context = context;
    }

    public override async Task<PermissionResponseDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {

        Permission newPermission = new Permission
        {
            Type = request.PermissionType
        };

        try
        {
            await _context.Permissions.AddAsync(newPermission);

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            throw new DbUpdateException("An error occured while saving permission to database", e);
        }


        return new PermissionResponseDto
        {
            Description = newPermission.Description,
            Name = newPermission.Name,
            Id = newPermission.Id
        };
    }
}