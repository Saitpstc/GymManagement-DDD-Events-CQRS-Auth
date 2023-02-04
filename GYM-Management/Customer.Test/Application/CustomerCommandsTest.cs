namespace Customer.Test.Application;

using Core;
using Customer.Application.Customer.Commands;
using FluentAssertions;
using Infrastructure.Repository;
using Moq;

public class CustomerCommandsTest:IClassFixture<CustomerRepository>
{
    private readonly Mock<ICustomerRepository> _repository;

    public CustomerCommandsTest()
    {
        _repository = new Mock<ICustomerRepository>();

    }


    [Fact]
    public void Creating_Customer_With_Mediatr_Handler_Should_Return_New_Customer_With_Minimum_Requirements()
    {


        _repository.Setup(repository => repository.Add())
        var command = new CreateCustomer.Command("name", "surname", "countryCode", "number", "mail");

        var resultCustomer = new Customer()
        var handler = new CreateCustomer.CommandHandler(_repository.Object);


        var result = handler.Handle(command, CancellationToken.None);


        result.Should().Be(typeof(Guid));

    }
}