namespace ASPNetExapp.Middlewares;

public class UserRequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public UserRequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Запит до {context.Request.Method} {context.Request.Path}");

        // Пропускаємо запит в наступний middleware
        await _next(context);
    }
}
