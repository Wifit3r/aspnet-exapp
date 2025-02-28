
namespace ASPNetExapp.Middlewares;

public class ServerErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ServerErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Пропускаємо запит в наступний middleware
        }
        catch (Exception ex)
        {
            // Логування помилки
            Console.WriteLine($"Помилка: {ex.Message}");

            // Встановлюємо статус відповіді на 500
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Сталася помилка.");
        }
    }
}
