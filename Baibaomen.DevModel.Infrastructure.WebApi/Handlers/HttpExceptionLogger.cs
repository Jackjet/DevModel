using Baibaomen.DevModel.Infrastructure;
using System;
using System.Data.Entity.Infrastructure;
using System.Web.Http.ExceptionHandling;

namespace Baibaomen.DevModel.Infrastructure
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
            //if (exceptionType != typeof(DbUpdateConcurrencyException))
            {
                exceptionCallback(exception);
            }
        }
    }
}