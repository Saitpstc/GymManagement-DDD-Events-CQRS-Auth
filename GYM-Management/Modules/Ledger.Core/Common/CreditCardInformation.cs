namespace Ledger.Core;

using System.ComponentModel.DataAnnotations;

public class CreditCardInformation
{
    [Required]
    public string CardNumber { get; set; }
    [Required]
    public string Cvc { get; set; }
    [Required]
    public long ExpMonth { get; set; }
    [Required]
    public long ExpYear { get; set; }
    [Required]
    public string CardFullName { get; set; }
}