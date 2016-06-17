using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Baibaomen.DevModel.Infrastructure
{
    public static class ModelStateErrorBuilder
    {
        public static string BuildErrorMessage(this ModelStateDictionary modelStates)
        {
            var errorMessages = (from modelState in modelStates.Values
                                           from error in modelState.Errors
                                           select error.ErrorMessage).ToList();
            return string.Join(" ", errorMessages);
        }
    }
}
