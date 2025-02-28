namespace ASPNetExapp.Middlewares;
public class SimpleAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public SimpleAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Зчитуємо значення заголовка x-auth
        var authHeader = context.Request.Headers["x-auth"].ToString();

        // Перевіряємо чи значення в заголовку x-auth дорівнює 12345
        if (authHeader != "12345")
        {
            // Якщо токен не співпадає, встановлюємо статус 401 (Unauthorized)
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        // Якщо автентифікація пройшла успішно, передаємо запит далі
        await _next(context);
    }
}
