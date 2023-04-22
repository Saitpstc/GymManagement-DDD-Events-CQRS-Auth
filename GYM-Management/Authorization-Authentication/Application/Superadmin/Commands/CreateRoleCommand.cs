namespace Authorization_Authentication.Application.Superadmin.Commands;

using Dto;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Application.Contracts;

public class CreateRoleCommand:ICommand<RoleResponseDto>
{
    public string Name { get; set; }
}

class CreateRoleCommandHandler:CommandHandlerBase<CreateRoleCommand, RoleResponseDto>
{
    private readonly AuthDbContext _context;
    private readonly RoleManager<Role> _roleManager;


    public CreateRoleCommandHandler(
        IErrorMessageCollector errorMessageCollector,
        RoleManager<Role> roleManager,
        AuthDbContext context):base(errorMessageCollector)
    {
        _roleManager = roleManager;
        _context = context;

    }

    public override async Task<RoleResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Role newRole = new Role
        {
            Name = request.Name,
            IsActive = true
        };

        await _context.SaveChangesAsync(cancellationToken);
        IdentityResult? result = await _roleManager.CreateAsync(newRole);

        if (!result.Succeeded)
        {
            foreach (IdentityError? error in result.Errors)
            {
                ErrorMessageCollector.AddError(error.Description);
            }
            return null;
        }

        return new RoleResponseDto
        {

            Id = newRole.Id,
            Name = newRole.Name
        };
    }
}