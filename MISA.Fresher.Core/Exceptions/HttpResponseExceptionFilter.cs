using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Core.Exceptions
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException httpResponseException)
            {
                //  TH: Xảy ra ngoại lệ
                var result = new
                {
                    msgDev = "",
                    userMsg = MISA.Fresher.Core.Properties.Resources.Exception,
                    data = httpResponseException.Value,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };

                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                var result = new
                {
                    msgDev = "",
                    userMsg = MISA.Fresher.Core.Properties.Resources.Exception,
                    data = DBNull.Value,
                    moreInfo = ""
                };
                context.Result = new ObjectResult(result)
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
