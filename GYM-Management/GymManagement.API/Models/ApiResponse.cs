namespace GymManagement.API.Models;

using Newtonsoft.Json;

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ApiResponse
{
    public bool IsSuccessfull { get; set; }
    public List<string> ErrorMessages { get; set; }
}

public class ApiResponse<T>:ApiResponse
{
    public T Data { get; set; }


}