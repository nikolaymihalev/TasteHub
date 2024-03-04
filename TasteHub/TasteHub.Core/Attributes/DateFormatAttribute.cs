using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TasteHub.Core.Attributes
{
    public class DateFormatAttribute : ValidationAttribute
    {
        private string format;

        public DateFormatAttribute(string _format)
        {
            format = _format;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null||string.IsNullOrEmpty(value.ToString())) 
            {
                return ValidationResult.Success;
            }

            DateTime result;

            if (!DateTime.TryParseExact(value.ToString(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)) 
            {
                return new ValidationResult(ErrorMessage ?? $"The date field must be in the format {format}");
            }

            return ValidationResult.Success;
        }
    }
}
