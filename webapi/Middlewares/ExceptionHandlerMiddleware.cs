using System.Net.Mime;
using apibanca.application.Exceptions;
using apibanca.webapi.Models;
using apibanca.webapi.Exceptions;

namespace apibanca.webapi.Middlewares;
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next) //, ILogger logger)
    {
        _next = next;
        //_logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = MediaTypeNames.Application.Json; 
        var responseModel = new ExceptionResponse()
        {
            Message = exception.Message
        };

        switch (exception)
        {
            case ApiException e:
                response.StatusCode = (int)StatusCodes.Status400BadRequest;
                break;
            case AppException e:
                response.StatusCode = (int)StatusCodes.Status400BadRequest;
                break;
            case AppValidationException e:
                response.StatusCode = (int)StatusCodes.Status400BadRequest;
                foreach ( var i in e.Errors)
                    responseModel.AddValidationError(i.PropertyName, i.ErrorMessage);
                break;
            case KeyNotFoundException e:
                responseModel.Message = "The record identifier doesn't exist";
                response.StatusCode = (int)StatusCodes.Status404NotFound;
                break;
            case DatabaseException e:
                responseModel.Message = "One or more error have happened in database operation." ;
                responseModel.AddError(e.Message+". "+e.InnerException?.Message??"");
                response.StatusCode = (int)StatusCodes.Status409Conflict;
                break;
            default:
                response.StatusCode = (int)StatusCodes.Status500InternalServerError;
                break;
        }
        await response.WriteAsync(responseModel.ToString());
    }
}
