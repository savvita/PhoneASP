using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Phone.Web.Exceptions;
using System.Net;

namespace Phone.Web
{
    public class AppExceptionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                if (context.Exception is PhoneNotFoundException)
                {
                    context.Result = new ObjectResult(new
                    {
                        code = "phone not found"
                    })
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
                else if (context.Exception is BadRequestException)
                {
                    context.Result = new ObjectResult(new
                    {
                        code = "bad-request"
                    })
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };
                }

                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
