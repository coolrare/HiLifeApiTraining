using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebApplication1.ActionFilters
{
    public class HilifeErrorViewModel
    {
        public string ErrorNo { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class ERR001Exception : Exception
    {
        public string ErrorNo { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class HilifeErrorHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            if (exception == null)
            {
                return;
            }

            if (exception is ERR001Exception)
            {

            }

            actionExecutedContext.Response =
                actionExecutedContext.Request.CreateResponse(
                    HttpStatusCode.InternalServerError,
                    new HilifeErrorViewModel()
                    {
                        ErrorNo = "ERR001",
                        ErrorMsg = exception.Message
                    });

            //base.OnException(actionExecutedContext);
        }
    }
}