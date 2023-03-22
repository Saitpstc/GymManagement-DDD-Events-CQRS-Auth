namespace Customer.Application.DTO.Response;

using Core.ValueObjects;

public class CustomerCreatedResponse
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}