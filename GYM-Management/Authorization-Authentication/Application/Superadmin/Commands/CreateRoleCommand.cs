namespace Authorization_Authentication.Application.Superadmin.Commands;

using Dto;
using Infrastructure;
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
    private readonly AuthUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(
        IErrorMessageCollector errorMessageCollector,
        RoleManager<Role> roleManager,
        AuthUnitOfWork unitOfWork):base(errorMessageCollector)
    {
        _roleManager = roleManager;
        _unitOfWork = unitOfWork;
    }

    public override async Task<RoleResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        Role newRole = new Role
        {
            Name = request.Name,
            IsActive = true
        };

        await _unitOfWork.SaveAsync(cancellationToken);
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