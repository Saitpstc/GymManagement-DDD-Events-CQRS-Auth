namespace Authorization_Authentication.Models;

class ConfirmationCode
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Code { get; set; }
    public DateTime Created { get; set; }
    public DateTime ValidTo { get; set; }
}