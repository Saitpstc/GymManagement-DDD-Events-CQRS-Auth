namespace Customer.Application.DTO.Response;

public class MembershipStartedResponse
{
    public string UserName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Period { get; set; }
}