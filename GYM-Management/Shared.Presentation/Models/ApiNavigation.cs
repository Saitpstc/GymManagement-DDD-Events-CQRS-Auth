namespace GymManagement.API.Models;

public class ApiNavigation
{
    public string EndPoint { get; set; }
    public string Action { get; set; }
    public string Operation { get; set; }
    public object AcceptedData { get; set; }
}