namespace Customer.Application.Customer.Commands;

using Core;
using Core.ValueObjects;
using DTO.Response;
using IntegrationEvents;
using Shared.Application.Contracts;

public class CreateCustomer
{

    public class Command:ICommand<CustomerCreatedResponse>
    {

        /*public Command(string name, string surname, string countrycode, string number, string mail)
        {
            _name = name;
            _surname = surname;
            _countrycode = countrycode;
            _number = number;
            _mail = mail;
        }*/

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Countrycode { get; set; }
        public string Number { get; set; }
        public string Mail { get; set; }

        public Guid Id { get; }
    }




    public class CreateCustomerCommandHandler:CommandHandlerBase<Command, CustomerCreatedResponse>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IErrorMessageCollector collector):base(collector)
        {
            _repository = repository;
        }

        public override async Task<CustomerCreatedResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            Customer customer = new Customer(new Name(request.Name, request.Surname), new PhoneNumber(request.Countrycode, request.Number),
                new Email(request.Mail));
            var AddedCustomer = await _repository.AddAsync(customer);
            await _repository.CommitAsync();


            var response = new CustomerCreatedResponse()
            {
                Id = AddedCustomer.Id,
                Name = customer.GetName().OfCustomer(),
                PhoneNumber = customer.GetNumber().Number(),
                Email = customer.GetMail()
            };
            return response;
        }
    }
}