using Customer.Core;
using Customer.Core.ValueObjects;
using MediatR;
using Shared.Application.Config.Commands;
using Shared.Application.Contracts;

namespace Customer.Application.Customer.Commands;

public class CreateCustomer
{

    public class Command:ICommand<bool>
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



    public class CreateCustomerHandler:CommandHandlerBase<CreateCustomer.Command, bool>
    {

        public override Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            ErrorMessageCollector.AddError("testingNewHandler");
            return Task.FromResult(true);
        }

        public CreateCustomerHandler(IErrorMessageCollector collector):base(collector)
        {
        }
    }
}