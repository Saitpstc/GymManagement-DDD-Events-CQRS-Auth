namespace Customer.Application.DTO.Response;

public class CustomerCreatedResponse
{

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid UserId { get; set; }
}