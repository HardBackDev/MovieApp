using System.ComponentModel.DataAnnotations;

namespace Shared.ValidationAttributes
{
    public class IsGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _minPropertyName;

        public IsGreaterThanAttribute(string minPropertyName)
        {
            _minPropertyName = minPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var minProperty = validationContext.ObjectType.GetProperty(_minPropertyName);

            if (minProperty == null)
            {
                return new ValidationResult($"Unknown property: {_minPropertyName} of class {validationContext.ObjectType.FullName}");
            }

            var minValue = (IComparable)minProperty.GetValue(validationContext.ObjectInstance);
            var maxValue = (IComparable)value;

            if (minValue != null && maxValue != null && minValue.CompareTo(maxValue) > 0)
            {
                return new ValidationResult(ErrorMessage ?? $"The value of {validationContext.DisplayName} must be greater than {_minPropertyName}");
            }

            return ValidationResult.Success;
        }
    }
}
