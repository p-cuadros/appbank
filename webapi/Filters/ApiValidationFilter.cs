using Microsoft.AspNetCore.Mvc.Filters;
using apibanca.application.Exceptions;

namespace apibanca.webapi.Filters;
public class ApiValidationFilter: ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
            throw new AppValidationException(errors);
        }
        base.OnActionExecuting(context);
    }
}