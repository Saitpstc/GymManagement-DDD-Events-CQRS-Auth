namespace GymManagement.API.Controllers;

using System.Net;
using global::Customer.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;
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

    protected ApiResponse<T> CreateResponse<T>(T response)
    {
        if (_collector.ErrorMessage.Any())
        {
            return ApiResponseFactory.Fail<T>(_collector.ErrorMessage);
        }

        return ApiResponseFactory.Success(response);
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
}