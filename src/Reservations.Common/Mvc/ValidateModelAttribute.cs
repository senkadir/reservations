using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Reservations.Common.Mvc
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public ValidateModelAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ServiceErrorContext errorContext = new ServiceErrorContext()
                {
                    Code = "bad_request",
                };

                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                                                              .SelectMany(v => v.Errors)
                                                              .Select(x => new ServiceError
                                                              {
                                                                  Exception = x.Exception != null ? x.Exception.ToString() : "",
                                                                  ErrorMessage = x.ErrorMessage
                                                              })
                                                              .ToList();

                errorContext.Errors = errors;

                context.Result = new BadRequestObjectResult(errorContext);
            }
        }
    }
}
