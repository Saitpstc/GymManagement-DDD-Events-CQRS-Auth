namespace Customer.Application.Customer.Commands;

using Core;
using Core.ValueObjects;
using DTO.Response;
using FluentValidation;
using MediatR;
using Shared.Application.Contracts;

public class CreateCustomerCommand:ICommand<CustomerCreatedResponse>
{


    public string Name { get; set; }
    public string Surname { get; set; }
    public string Countrycode { get; set; }
    public string Number { get; set; }
    public string Mail { get; set; }

    public Guid UserId { get; set; }

    public Guid Id { get; }
}

public class CommandValidator:AbstractValidator<CreateCustomerCommand>
{
    public CommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(command => command.Surname).NotEmpty().WithMessage("Surname is required");
        RuleFor(command => command.Countrycode).NotEmpty().WithMessage("Countrycode is required");
        RuleFor(command => command.Number).NotEmpty().WithMessage("Number is required");
        RuleFor(command => command.Mail).NotEmpty().EmailAddress().WithMessage("Mail is not valid");
        RuleFor(command => command.UserId).NotEmpty().NotEqual(Guid.Empty).WithMessage("UserId is required");
    }
}

public class CreateCustomerCommandHandler:CommandHandlerBase<CreateCustomerCommand, CustomerCreatedResponse>
{
    private readonly ICustomerRepository _repository;
    private IMediator _mediator;

    public CreateCustomerCommandHandler(ICustomerRepository repository, IErrorMessageCollector collector, IMediator mediator):base(collector)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public override async Task<CustomerCreatedResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        Customer customer = new Customer(new Name(request.Name, request.Surname), new PhoneNumber(request.Countrycode, request.Number),
            new Email(request.Mail), request.UserId);
        var AddedCustomer = await _repository.AddAsync(customer);
        await _repository.CommitAsync();



        var response = new CustomerCreatedResponse()
        {
            Id = AddedCustomer.Id,
            UserId = request.UserId,
            Name = customer.Name.OfCustomer(),
            PhoneNumber = customer.PhoneNumber.ToString(),
            Email = customer.Email.ToString()
        };
        return response;
    }
}