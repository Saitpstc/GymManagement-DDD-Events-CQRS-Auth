using Customer.Core;
using MediatR;
using Shared.Application.Config.Commands;
using Shared.Application.Contracts;

namespace Customer.Application.Customer.Commands;

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

        public CommandHandler(ICustomerRepository repository)
        {
            _repository = repository;

        }

        public Task<Unit> Handle(CreateCustomer.Command request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}