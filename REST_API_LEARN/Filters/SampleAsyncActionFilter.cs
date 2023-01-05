using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;
using REST_API_LEARN.DB;

namespace REST_API_LEARN.Filters
{
    public class SampleAsyncActionFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
