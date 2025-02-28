using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetExapp.Middlewares;

public class UserIdValidationActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userId = context.RouteData.Values["id"]?.ToString();

        if (string.IsNullOrEmpty(userId) || !IsValidUserId(userId))
        {
            context.Result = new BadRequestObjectResult("Invalid user ID");
        }

        base.OnActionExecuting(context);
    }

    private bool IsValidUserId(string userId)
    {
        // Логіка перевірки правильності ідентифікатора користувача
        return int.TryParse(userId, out int id) && id > 0;  // Приклад перевірки: ID має бути числом більше 0
    }
}
