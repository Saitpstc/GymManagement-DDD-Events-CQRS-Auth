namespace Customer.Test.Application;

using Core;
using Moq;

public class CustomerCommandsTest
{
    private readonly Mock<ICustomerRepository> _repository;

    public CustomerCommandsTest()
    {
        _repository = new Mock<ICustomerRepository>();

    }


    /*[Fact]
    public void Creating_Customer_With_Mediatr_Handler_Should_Return_New_Customer_With_Minimum_Requirements()
    {

        var customer = new Customer(new Name("name", "surname"), new PhoneNumber("90", "1234567891"), new Email("test@gmail.com"));

        
        _repository.Setup(repository => repository.AddAsync(customer)).ReturnsAsync(customer);
        var command = new CreateCustomer.Command("name", "surname", "90", "1234567891", "test@mail");
        
      var handler = new CreateCustomer.CommandHandler(_repository.Object);


        var result = handler.Handle(command, CancellationToken.None).GetAwaiter().GetResult();


        result.Should().Be(Unit.Value);

    }*/
}