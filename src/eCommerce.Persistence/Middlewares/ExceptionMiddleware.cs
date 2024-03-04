namespace eCommerce.Persistence.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        int statusCode = GetStatusCode(exception);
        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";
        var response = new Response()
        {
            Message = exception.Message,
            StatusCode = statusCode,
            IsSuccess = false
        };

        var result = JsonConvert.SerializeObject(response);

        return httpContext.Response.WriteAsync(result);
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            System.ComponentModel.DataAnnotations.ValidationException
                => StatusCodes.Status400BadRequest,
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            FastEndpoints.ValidationFailureException => StatusCodes.Status400BadRequest,
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            DatabaseConflictException => StatusCodes.Status409Conflict,
            SaveChangesException => StatusCodes.Status409Conflict,
            CreateEntityException => StatusCodes.Status409Conflict,
            DatabaseExecuteQueryException => StatusCodes.Status409Conflict,
            DatabaseTransactionException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
}
