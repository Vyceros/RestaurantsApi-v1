
using Microsoft.AspNetCore.Http.HttpResults;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Api.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next.Invoke(context);
		}
		catch(NotFoundException notFound)
		{
            logger.LogWarning(notFound,notFound.Message);

            context.Response.StatusCode = 404;
			await context.Response.WriteAsync(notFound.Message);
		}
		catch(ForbidException forbiddenOperation)
		{
			context.Response.StatusCode = 403;
			await context.Response.WriteAsync("Access forbidden");
		}
		catch (Exception ex)
		{

			logger.LogError(ex, ex.Message);

			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("Something went wrong..");
		}
    }
}
