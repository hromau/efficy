using Efficy.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Efficy;

public class ExistsValidationAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbContext = await context.HttpContext.RequestServices.GetService<IDbContextFactory<EfficyDbContext>>()
            ?.CreateDbContextAsync();
        if (dbContext == null)
        {
            context.Result = new BadRequestResult();
        }

        var id = context.RouteData.Values["id"]?.ToString();
        if (string.IsNullOrEmpty(id))
        {
            context.Result = new BadRequestObjectResult("Please provide a team id");
        }

        var team = await dbContext?.Teams.FirstOrDefaultAsync(t => t.Id == Guid.Parse(id));
        var counter = await dbContext?.Counters.FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
        if (team == null && counter == null)
        {
            context.Result = new NotFoundResult();
            return;
        }

        await base.OnActionExecutionAsync(context, next);
    }
}