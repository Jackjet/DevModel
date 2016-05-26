using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StarLight.Saas.Infrastructure.Utility.Validation
{
    public static class ValidatorHelper
    {
        public static bool TryValidate(object obj, out ICollection<ValidationResult> validationResults)
        {
            var context = new ValidationContext(obj, null, null);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(obj, context, validationResults, true);
        }
    }
}