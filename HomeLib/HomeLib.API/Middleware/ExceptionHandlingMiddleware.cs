using HomeLib.API.DataTypes;
using HomeLib.Core.Exceptions;

namespace HomeLib.API.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        var errorResponse = new ErrorResponse()
        {
            Message = exception.Message,
        };

        switch (exception)
        {
            case NotFoundException:
                response.StatusCode = StatusCodes.Status404NotFound;
                errorResponse.Details = "Not Found";
                logger.LogError(exception, exception.Message);
                break;
            case BadRequestException:
                response.StatusCode = StatusCodes.Status400BadRequest;
                errorResponse.Details = "Bad Request";
                logger.LogError(exception, exception.Message);
                break;
            case IncorrectOldPasswordException:
                response.StatusCode = StatusCodes.Status403Forbidden;
                errorResponse.Details = "Incorrect old password";
                logger.LogError(exception, exception.Message);
                break;
            case AlreadyAddedException:
                response.StatusCode = StatusCodes.Status409Conflict;
                errorResponse.Details = "Already Added";
                logger.LogError(exception, exception.Message);
                break;
            case UnauthorizedException: 
                response.StatusCode = StatusCodes.Status401Unauthorized;
                errorResponse.Details = "Unauthorized";
                logger.LogError(exception, exception.Message);
                break;
            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                errorResponse.Details = exception.Message;
                logger.LogError(exception, "Unhandled exception");
                break;
        }
        errorResponse.StatusCode = response.StatusCode;
        await response.WriteAsJsonAsync(errorResponse);
    }
}