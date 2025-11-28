using Microsoft.AspNetCore.Mvc;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message) { }
}

public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}

public class GlobalExceptionHandler(
    RequestDelegate requestDelegate,
    ILogger<GlobalExceptionHandler> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await requestDelegate(httpContext);
        }
        catch (Exception e)
        {
            // Log the full exception details (like Foundation does)
            logger.LogError(e, "Exception caught from API operation");

            var (statusCode, problemDetails) = e switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, CreateProblemDetails(
                    "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
                    StatusCodes.Status404NotFound,
                    "Not Found",
                    "The requested resource was not found")),
                
                BadRequestException => (StatusCodes.Status400BadRequest, CreateProblemDetails(
                    "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
                    StatusCodes.Status400BadRequest,
                    "Bad Request",
                    "The request was invalid")),
                
                UnauthorizedException => (StatusCodes.Status401Unauthorized, CreateProblemDetails(
                    "https://www.rfc-editor.org/rfc/rfc7235#section-3.1",
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    "Authentication is required")),
                
                ForbiddenException => (StatusCodes.Status403Forbidden, CreateProblemDetails(
                    "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.3",
                    StatusCodes.Status403Forbidden,
                    "Forbidden",
                    "Access to the resource is forbidden")),
                
                ConflictException => (StatusCodes.Status409Conflict, CreateProblemDetails(
                    "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.8",
                    StatusCodes.Status409Conflict,
                    "Conflict",
                    "The request conflicts with the current state")),
                
                // Default case - don't expose internal details (security best practice)
                _ => (StatusCodes.Status500InternalServerError, CreateProblemDetails(
                    "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1",
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error",
                    "An internal server error occurred"))
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    private static ProblemDetails CreateProblemDetails(string type, int status, string title, string detail)
    {
        return new ProblemDetails
        {
            Type = type,
            Status = status,
            Title = title,
            Detail = detail
        };
    }
}
