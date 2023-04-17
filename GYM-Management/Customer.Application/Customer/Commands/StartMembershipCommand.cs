namespace Customer.Application.Customer.Commands;

using System.ComponentModel;
using Core;
using Core.Enums;
using Core.ValueObjects;
using DTO.Response;
using FluentValidation;
using IntegrationEvents.CustomerModule;
using MediatR;
using Shared.Application.Contracts;
using Shared.Core.Exceptions;

public class StartMembershipCommand:ICommand<MembershipStartedResponse>
{
    public Guid CustomerId { get; set; }
    public DateTime EndDate { get; set; }
}

public class StartMembershipCommandValidator:AbstractValidator<StartMembershipCommand>
{
    public StartMembershipCommandValidator()
    {
        RuleFor(command => command.CustomerId)
            .NotEmpty();

        RuleFor(command => command.EndDate)
            .GreaterThanOrEqualTo(command => DateTime.Now);
    }
}

internal class StartMembershipCommandHandler:CommandHandlerBase<StartMembershipCommand, MembershipStartedResponse>
{
    private readonly ICustomerRepository _repository;
    private readonly IMediator _mediator;

    public StartMembershipCommandHandler(
        IErrorMessageCollector errorMessageCollector,
        ICustomerRepository repository,
        IMediator mediator):base(errorMessageCollector)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public override async Task<MembershipStartedResponse> Handle(StartMembershipCommand request, CancellationToken cancellationToken)
    {

        Membership membership = Membership.CreateNew(DateTime.Now, request.EndDate);

        var customer = await _repository.RetriveByAsync(request.CustomerId);

        if (customer is null)
        {
            throw new BusinessLogicException("Customer is not found to create membership");
        }

        customer.StartMembership(membership);



        await _repository.UpdateAsync(customer);
        await _repository.CommitAsync();

        MembershipCreatedEvent @event = new MembershipCreatedEvent()
        {
            CustomerId = customer.Id,
            MembershipEndDate = membership.EndDate,
            MembershipStartDate = membership.StartDate
        };
        await _mediator.Publish(@event, cancellationToken);

        //todo: change username to the User's username
        return new MembershipStartedResponse()
        {

            EndDate = membership.EndDate,
            StartDate = membership.StartDate,
            UserName = customer.Name.OfCustomer()
        };
    }
}