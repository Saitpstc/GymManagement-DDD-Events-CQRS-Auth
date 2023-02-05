

using Customer.Core;
using Customer.Core.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Application.Config.Commands;
using Shared.Application.Contracts;
using Serilog;
using ILogger = Serilog.ILogger;

public class CreateCustomer
{

    public class Command:ICommand
    {
        public string _name { get; }
        public string _surname { get; }
        public string _countrycode { get; }
        public string _number { get; }
        public string _mail { get; }

        public Command(string name, string surname, string countrycode, string number, string mail)
        {
            _name = name;
            _surname = surname;
            _countrycode = countrycode;
            _number = number;
            _mail = mail;
        }

        public Guid Id { get; }
    }



    internal class CommandHandler:ICommandHandler<CreateCustomer.Command>
    {
        private readonly ICustomerRepository _repository;
        private readonly ILogger _logger;

        public CommandHandler(ICustomerRepository repository, ILogger logger )
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateCustomer.Command request, CancellationToken cancellationToken)
        {
            var customer = new Customer.Core.Customer(new Name(request._name, request._surname), new PhoneNumber(request._countrycode, request._number),
                new Email(request._mail));

            _logger.Warning("testing database log");
            await _repository.Add(customer);

            return Unit.Value;
        }
    }
}