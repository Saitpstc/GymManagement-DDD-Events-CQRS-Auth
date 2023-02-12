namespace Shared.Infrastructure;

using System.ComponentModel;

public enum RequestType
{
    [Description("POST")]
    Post,
    [Description("PATCH")]
    Patch,
    [Description("PUT")]
    Put,
    [Description("GET")]
    Get,
    [Description("DELETE")]
    Delete,

}