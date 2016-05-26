using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace StarLight.Saas.Infrastructure.Utility.Validation
{
    public class CompareToAttribute : ValidationAttribute
    {
        /// <summary>
        ///     Creates the attribute
        /// </summary>
        /// <param name="otherProperty">The other property to compare to</param>
        /// <param name="criteria"></param>
        public CompareToAttribute(string otherProperty, CompareType criteria)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException(nameof(otherProperty));
            }
            OtherProperty = otherProperty;
            Criteria = criteria;
        }
        /// <summary>
        ///     The other property to compare to
        /// </summary>
        public string OtherProperty { get; set; }

        public CompareType Criteria { get; set; }

        /// <summary>
        ///     Determines whether the specified value of the object is valid.  For this to be the case, the objects must be of the
        ///     same type
        ///     and satisfy the comparison criteria. Null values will return false in all cases except when both
        ///     objects are null.  The objects will need to implement IComparable for the GreaterThan,LessThan,GreatThanOrEqualTo
        ///     and LessThanOrEqualTo instances
        /// </summary>
        /// <param name="value">The value of the object to validate</param>
        /// <param name="validationContext">The validation context</param>
        /// <returns>A validation result if the object is invalid, null if the object is valid</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            // the the other property
            var property = validationContext.ObjectType.GetProperty(OtherProperty);

            // check it is not null
            if (property == null)
                return new ValidationResult($"Unknown property: {OtherProperty}.");

            // check types
            var memberName =
                validationContext.ObjectType.GetProperties()
                    .Where(
                        p =>
                            p.GetCustomAttributes(false)
                                .OfType<DisplayAttribute>()
                                .Any(a => a.Name == validationContext.DisplayName))
                    .Select(p => p.Name)
                    .FirstOrDefault() ?? validationContext.DisplayName;
            if (validationContext.ObjectType.GetProperty(memberName).PropertyType != property.PropertyType)
                return
                    new ValidationResult($"The types of {memberName} and {OtherProperty} must be the same.");

            // get the other value
            var other = property.GetValue(validationContext.ObjectInstance, null);
            if (other == null)
            {
                return null;
            }

            // equals to comparison,
            if (Criteria == CompareType.EqualTo)
            {
                if (Equals(value, other))
                    return null;
            }
            else if (Criteria == CompareType.NotEqualTo)
            {
                if (!Equals(value, other))
                    return null;
            }
            else
            {
                // check that both objects are IComparables
                if (!(value is IComparable) || !(other is IComparable))
                    return
                        new ValidationResult(
                            $"{validationContext.DisplayName} and {OtherProperty} must both implement IComparable");

                // compare the objects
                var result = Comparer.Default.Compare(value, other);

                switch (Criteria)
                {
                    case CompareType.GreaterThan:
                        if (result > 0)
                            return null;
                        break;
                    case CompareType.LessThan:
                        if (result < 0)
                            return null;
                        break;
                    case CompareType.GreatThanOrEqualTo:
                        if (result >= 0)
                            return null;
                        break;
                    case CompareType.LessThanOrEqualTo:
                        if (result <= 0)
                            return null;
                        break;
                }
            }

            // got this far must mean the items don't meet the comparison criteria
            return new ValidationResult(ErrorMessage);
        }
    }

    /// <summary>
    ///     Indicates a comparison criteria used by the CompareValues attribute
    /// </summary>
    public enum CompareType
    {
        EqualTo,
        NotEqualTo,
        GreaterThan,
        LessThan,
        GreatThanOrEqualTo,
        LessThanOrEqualTo
    }
}