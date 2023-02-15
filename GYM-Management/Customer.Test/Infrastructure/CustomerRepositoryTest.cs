namespace Customer.Test.Infrastructure;

using Core;
using Core.ValueObjects;
using Customer.Infrastructure.Database;
using Customer.Infrastructure.Repository;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

public class CustomerRepositoryTest
{


    public CustomerRepositoryTest()
    {

       
    }


    [Fact]
    public void Get_Customer_Should_Return_One_Customer_Aggregate()
    {
        
    }
    [Fact]
    public void  CreateCustomer_Should_Add_One_Record_To_Database()
    {
        
        var options = new DbContextOptionsBuilder<CustomerDbContext>()
                      .UseInMemoryDatabase(databaseName: "InMemoryDb")
                      .Options;

        var mediator = new Mock<IMediator>().Object;

        int count = 0;
        
         using (var context = new CustomerDbContext(options,mediator))
        {
            var repository = new CustomerRepository(context);
            var result=repository.Add(new Customer(new Name("sait", "postaci"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com")));

            count = context.Customers.Count();
        }

        count.Should().Be(1);
    }
}