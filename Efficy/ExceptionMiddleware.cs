namespace Efficy;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionMiddleware>>();
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            logger.LogError("Something went wrong {Error}", e.Message);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync(e.Message);
        }
    }
}