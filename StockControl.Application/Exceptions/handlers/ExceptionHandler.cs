using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using StockControl.Application.Requests;

namespace StockControl.Application.Exceptions.handlers;

public class ExceptionHandler : ExceptionFilterAttribute
{
    private static readonly ILogger<ExceptionHandler> _logger;
    private static readonly Dictionary<string, HttpStatusCode> StatusTable = new();

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is { } exception)
        {
            _logger.LogError("Exception handled: {Message}", exception.Message);

            var statusCode = MapStatus(exception);
            var response = new ErrorResponse((int)statusCode, exception.Message);

            context.Result = new JsonResult(response)
            {
                StatusCode = (int)statusCode
            };
        }
    }

    private HttpStatusCode MapStatus(Exception ex)
    {
        return StatusTable.TryGetValue(ex.GetType().Name, out var statusCode)
            ? statusCode
            : HttpStatusCode.InternalServerError;
    }
}