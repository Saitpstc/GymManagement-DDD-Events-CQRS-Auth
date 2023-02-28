namespace Authorization_Authentication.Application.Superadmin.Commands;

using Dto;
using Microsoft.AspNetCore.Identity;
using Models;
using Shared.Application.Contracts;

public class CreateRoleCommand:ICommand<IDto>
{
    public string Name { get; set; }
}

public class CreateRoleCommandHandler:CommandHandlerBase<CreateRoleCommand, IDto>
{
    private readonly RoleManager<Role> _roleManager;

    public CreateRoleCommandHandler(IErrorMessageCollector errorMessageCollector, RoleManager<Role> roleManager):base(errorMessageCollector)
    {
        _roleManager = roleManager;
    }

    public override async Task<IDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var newRole = new Role()
        {
            Name = request.Name
        };
        var result = await _roleManager.CreateAsync(newRole);

        if (result.Succeeded)
        {
            return new RoleResponseDto()
            {
                Id = newRole.Id,
                Name = newRole.Name
            };
        }

        foreach (var error in result.Errors)
        {
            ErrorMessageCollector.AddError(error.Description);
        }
        return HandlerResponse.EmptyResultDto();
    }
}