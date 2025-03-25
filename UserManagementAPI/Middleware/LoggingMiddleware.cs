using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log the incoming request
        _logger.LogInformation("Incoming Request: {Method} {Path}", context.Request.Method, context.Request.Path);

        var stopwatch = Stopwatch.StartNew();

        // Call the next middleware in the pipeline
        await _next(context);

        stopwatch.Stop();

        // Log the outgoing response
        _logger.LogInformation("Outgoing Response: {StatusCode} in {ElapsedMilliseconds}ms", context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
    }
}