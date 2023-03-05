namespace Customer.Test.Infrastructure;

using Core;
using Core.ValueObjects;
using Customer.Infrastructure.Database;
using Customer.Infrastructure.Database.Tables;
using Customer.Infrastructure.Repository;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;

public class CustomerRepositoryTest
{

    private readonly CustomerDbContext Context;

    public CustomerRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<CustomerDbContext>()
                      .UseInMemoryDatabase(databaseName: "InMemoryDb")
                      .Options;
        var mediator = new Mock<IMediator>().Object;
        Context = new CustomerDbContext(options);

    }




    [Fact]
    public void CreateCustomer_Should_Add_One_Record_To_Database()
    {
        //Arrange
        var repository = new CustomerRepository(Context);



        //Act
        var customer = Task.Run(()
            => repository.AddAsync(new Customer(new Name("sait", "postaci"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com")))
        ).Result;


        var customerDbs = Context.Customers.First(c => c.Id == customer.Id);

        //Assert
        customerDbs.Should().NotBeNull();
        customerDbs.Id.Should().Be(customer.Id);

    }

    [Fact]
    public void Get_Customer_Should_Return_One_Customer_Aggregate()
    {

        //Arrange
        var repository = new CustomerRepository(Context);
        var result = Task.Run(()
            => repository.AddAsync(new Customer(new Name("sait", "postaci"), new PhoneNumber("90", "5435288568"), new Email("sait@gmail.com")))
        ).Result;

        var Id = result.Id;

        //Act
        var customer = Task.Run(() => repository.RetriveByAsync(Id)).Result;


        //Assert
        customer.Id.Should().Be(Id);

    }

    [Fact]
    public void Delete_Customer_Should_Perform_Soft_Delete()
    {
        Context.Customers.Add(CustomerDB.FromDomainModel(new Customer(new Name("sait", "postaci"), new PhoneNumber("90", "5435288568"),
            new Email("sait@gmail.com"))));
        Context.SaveChanges();
        //Arrange
        var repository = new CustomerRepository(Context);
        var customerToDelete = Context.Customers.First();

        Context.Entry(customerToDelete).State = EntityState.Detached;
        //Act

        var result = Task.Run(() => repository.DeleteByAsync(customerToDelete.FromEntity()));
        var customer = Context.Customers.Find(customerToDelete.Id);

        //Assert
        result.Should().Be(true);
        customer.Should().NotBeNull();


    }
    
}