using Baibaomen.DevModel.Infrastructure;
using System;
using System.Web.Http.ExceptionHandling;

namespace Baibaomen.DevModel.ApiSite
{
    public class HttpExceptionLogger: ExceptionLogger
    {
        Action<Exception> exceptionCallback;

        public HttpExceptionLogger(Action<Exception> exceptionCallback) {
            this.exceptionCallback = exceptionCallback;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            var exception = context.Exception;
            var exceptionType = exception.GetType();
            if (exceptionType != typeof(SimpleBadRequestException))
            {
                exceptionCallback(exception);
            }
        }
    }
}