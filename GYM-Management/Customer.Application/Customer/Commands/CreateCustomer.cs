using Customer.Core;
using Customer.Core.ValueObjects;
using MediatR;
using Shared.Application.Config.Commands;
using Shared.Application.Contracts;

namespace Customer.Application.Customer.Commands;

using Customer = global::Customer.Core.Customer;

public class CreateCustomer
{

    public class Command:ICommand<Customer>
    {
        public string _name { get; set; }
        public string _surname { get; set; }
        public string _countrycode { get; set; }
        public string _number { get; set; }
        public string _mail { get; set; }

        /*public Command(string name, string surname, string countrycode, string number, string mail)
        {
            _name = name;
            _surname = surname;
            _countrycode = countrycode;
            _number = number;
            _mail = mail;
        }*/

        public Command()
        {

        }

        public Guid Id { get; }
    }




    public class CreateCustomerHandler:CommandHandlerBase<CreateCustomer.Command, Customer>
    {
        private readonly ICustomerRepository _repository;

        public override Task<Customer> Handle(Command request, CancellationToken cancellationToken)
        {
            var customer = new Customer(new Name(request._name, request._surname), new PhoneNumber(request._countrycode, request._number),
                new Email(request._mail));
            var AddedCustomer = _repository.AddAsync(customer);
            return AddedCustomer;
        }

        public CreateCustomerHandler(ICustomerRepository repository, IErrorMessageCollector collector):base(collector)
        {
            _repository = repository;
        }
    }
}

public class test
{

}