using Customer.Core;
using Customer.Core.ValueObjects;
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

        public Command()
        {
            
        }

        public Guid Id { get; }
    }



    public class CommandHandler:ICommandHandler<CreateCustomer.Command>
    {
        private readonly ICustomerRepository _repository;


        public CommandHandler(ICustomerRepository repository)
        {
            _repository = repository;

        }

        public async Task<Unit> Handle(CreateCustomer.Command request, CancellationToken cancellationToken)
        {
            var customer = new global::Customer.Core.Customer(new Name(request._name, request._surname),
                new PhoneNumber(request._countrycode, request._number),
                new Email(request._mail));
            
            await _repository.AddAsync(customer);

            return Unit.Value;
        }
    }
}