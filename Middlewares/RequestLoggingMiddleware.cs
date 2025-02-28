namespace ASPNetExapp.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        Console.WriteLine($"Запит до {context.Request.Method} {context.Request.Path} почався о {startTime}");

        // Пропускаємо запит в наступний middleware
        await _next(context);

        var endTime = DateTime.UtcNow;
        Console.WriteLine($"Запит до {context.Request.Method} {context.Request.Path} завершився о {endTime}");
        Console.WriteLine($"Час обробки запиту: {endTime - startTime}");
    }
}
