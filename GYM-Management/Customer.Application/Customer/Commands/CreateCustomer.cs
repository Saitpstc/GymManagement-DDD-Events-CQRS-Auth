namespace Customer.Application.Customer.Commands;

using Core;
using Core.ValueObjects;
using DomainEvents;
using Shared.Application.Contracts;

public class CreateCustomer
{

    public class Command:ICommand<Customer>
    {

        /*public Command(string name, string surname, string countrycode, string number, string mail)
        {
            _name = name;
            _surname = surname;
            _countrycode = countrycode;
            _number = number;
            _mail = mail;
        }*/

        public string _name { get; set; }
        public string _surname { get; set; }
        public string _countrycode { get; set; }
        public string _number { get; set; }
        public string _mail { get; set; }

        public Guid Id { get; }
    }




    public class CreateCustomerCommandHandler:CommandHandlerBase<Command, Customer>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IErrorMessageCollector collector):base(collector)
        {
            _repository = repository;
        }

        public override Task<Customer> Handle(Command request, CancellationToken cancellationToken)
        {
            Customer customer = new Customer(new Name(request._name, request._surname), new PhoneNumber(request._countrycode, request._number),
                new Email(request._mail));
            var AddedCustomer = _repository.AddAsync(customer);
            integratioEvent newCustomerCreate = new integratioEvent
            {
                CustomerId = Guid.NewGuid(),
                password = "password",
                UserName = "user@gmail.com"
            };
            return AddedCustomer;
        }
    }
}

public class test
{
}