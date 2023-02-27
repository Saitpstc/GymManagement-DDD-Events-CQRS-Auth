namespace Shared.Core;

public class AppOptions
{
    public List<ConnectionStrings> ConnectionStrings { get; set; }
    public string AuthTokenKey { get; set; }


    public string GetConnectionString(Modules module)
    {
        return ConnectionStrings.First(x => x.Module == module).ConnectionString;
    }
}

public class ConnectionStrings
{
    public Modules Module { get; set; }
    public string ConnectionString { get; set; }
}