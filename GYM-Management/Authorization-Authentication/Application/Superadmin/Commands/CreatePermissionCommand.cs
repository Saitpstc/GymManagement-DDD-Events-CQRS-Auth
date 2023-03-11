namespace Authorization_Authentication.Application.Superadmin.Commands;

using Dto;
using FluentValidation;
using Infrastructure.Database;
using Models;
using Shared.Application.Contracts;

public class CreatePermissionCommand:ICommand<PermissionResponseDto>
{

    public string Name { get; set; }
    public string Description { get; set; }
}

public class CreatePermissionValidator:AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
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
            Description = request.Description,
            Name = request.Name
        };
        await _context.Permissions.AddAsync(newPermission);

        await _context.SaveChangesAsync(cancellationToken);

        return new PermissionResponseDto
        {
            Description = newPermission.Description,
            Name = newPermission.Name,
            Id = newPermission.Id
        };
    }
}