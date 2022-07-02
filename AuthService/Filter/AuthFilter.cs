using AuthService.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Filter
{
    /// <summary>
    /// Global add header filter
    /// </summary>
    public class AuthFilter : IResultFilter
    {
        public AuthFilter()
        {
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        /// <summary>
        /// CreatedBy: SWE Team
        /// </summary>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            var bearerToken = context.HttpContext.Request.Headers[HeaderNames.Authorization];
            if (bearerToken.ToString() != "")
            {
                var inBlackList = MemoryCacheHelper.Get(bearerToken);
                if (inBlackList != null)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        ContentType = "application/json"
                    };
                    return;
                }
            }

        }
    }
}
