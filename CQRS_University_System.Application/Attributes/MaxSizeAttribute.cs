using System.ComponentModel.DataAnnotations;

namespace CQRS_University_System.Application.Attributes
{
    public class MaxSizeAttribute : ValidationAttribute
    {
        private readonly int _maxLength;
        public MaxSizeAttribute(int maxLength)
        {
            _maxLength = maxLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var property = value as string;

            if (property != null) 
            {
                if (property.Length > _maxLength) 
                {
                    return new ValidationResult($"Maximum allowed Length is {_maxLength} char");
                }
            }

            return ValidationResult.Success;
        }
    }
}


