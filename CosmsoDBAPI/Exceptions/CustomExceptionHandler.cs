

using System.Security.Cryptography;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

public class CustomExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        ProblemDetails problemDetails = exception switch
        {
            CosmosException => CosmosExceptions(exception),
            _ => CreateProblemDetails(StatusCodes.Status500InternalServerError, "Internal Server Exception ", exception.Message)

        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private static ProblemDetails CosmosExceptions(Exception exception)
    {
        var ce = exception as CosmosException;
        return CreateProblemDetails((int)ce?.StatusCode, "CosmosDbException", ce.Message);

    }

    private static ProblemDetails CreateProblemDetails(int status, string title, string details)
    {
        return new ProblemDetails()
        {
            Status = status,
            Title = title,
            Detail = details
        };
    }
}