using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net.Http.Headers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Baibaomen.DevModel.Infrastructure
{
    public class UnhandledExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(SimpleBadRequestException))
            {
                var errorMessage = new ErrorMessage { ErrorCode = 0, Message = exception.Message };
                context.Result = new SimpleBadRequestResult(context.Request, errorMessage);
            }
            else if (exceptionType == typeof(SimpleUnauthorizedException))
            {
                var authenticationHeaderValues = new List<AuthenticationHeaderValue> { context.Request.Headers.Authorization };
                context.Result = new UnauthorizedResult(authenticationHeaderValues, context.Request);
            }
            else if (exceptionType == typeof(DbUpdateConcurrencyException))
            {
                var errorMessage = new ErrorMessage { ErrorCode = 0, Message = exception.Message};
                context.Result = new SimpleConflictResult(context.Request,errorMessage);
            }
            else
            {
                context.Result = new SimpleInternalServerErrorResult(context.Request, exception.Message);
            }
        }
    }
}