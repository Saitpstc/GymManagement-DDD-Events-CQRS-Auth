namespace Customer.Infrastructure;

using Core;
using Shared.Infrastructure;

public class test:AppDbContext,testa
{
    private readonly ICustomerRepository _repository;

    public test(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public void test()
    {
        _repository.
    }
}

public class testa
{
    
}