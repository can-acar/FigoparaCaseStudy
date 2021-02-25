using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace FigoparaCaseStudyApi.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                                    .SelectMany(v => v.Errors)
                                    .Select(v => v.ErrorMessage)
                                    .ToList();

                var serializer = new JsonSerializer()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var errorsObject = new Dictionary<string, object>();


                foreach (var key in context.ModelState.Keys)
                {
                    foreach (var error in context.ModelState[key].Errors)
                    {
                        errorsObject[key] = new
                        {
                         
                            message  = error.ErrorMessage
                        };
                    }
                }


                var responseObj = new
                {
                    //Message = "Bad Request",
                    Status  = false,
                    Message = "",
                    Errors  = errorsObject
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}