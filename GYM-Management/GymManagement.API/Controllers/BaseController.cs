namespace GymManagement.API.Controllers;

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Application.Contracts;
using Shared.Presentation.Models;

[ApiController]
public class BaseController:ControllerBase, IActionFilter
{
    private readonly ErrorMessageCollector _collector;




    public BaseController(IErrorMessageCollector collector)
    {
        _collector = (ErrorMessageCollector) collector;

    }

    [NonAction]
    public void OnActionExecuting(ActionExecutingContext context)
    {

    }

    [NonAction]
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.ModelState.IsValid == false)
        {
            context.Result =
                new JsonResult(
                    ApiResponseFactory.Fail<string>(context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()));
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
        }

    }

    protected ApiResponse<T> CreateResponse<T>(T response)
    {
        if (_collector.ErrorMessage.Any())
        {
            return ApiResponseFactory.Fail<T>(_collector.ErrorMessage);
        }

        return ApiResponseFactory.Success(response);
    }
}