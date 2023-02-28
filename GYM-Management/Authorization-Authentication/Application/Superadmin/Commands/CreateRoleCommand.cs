namespace Authorization_Authentication.Application.Superadmin.Commands;

using Dto;
using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Application.Contracts;

public class CreateRoleCommand:ICommand<RoleResponseDto>
{
    public string Name { get; set; }
}

public class CreateRoleCommandHandler:CommandHandlerBase<CreateRoleCommand, RoleResponseDto>
{
    private readonly RoleManager<Role> _roleManager;

    public CreateRoleCommandHandler(IErrorMessageCollector errorMessageCollector, RoleManager<Role> roleManager):base(errorMessageCollector)
    {
        _roleManager = roleManager;
    }

    public override async Task<RoleResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var newRole = new Role()
        {
            Name = request.Name,
            IsActive = true
        };

        var result = await _roleManager.CreateAsync(newRole);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ErrorMessageCollector.AddError(error.Description);
            }
            return null;
        }

        return new RoleResponseDto()
        {

            Id = newRole.Id,
            Name = newRole.Name
        };
    }
}