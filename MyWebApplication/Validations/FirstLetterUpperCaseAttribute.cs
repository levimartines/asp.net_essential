using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Validations;

public class FirstLetterUpperCaseAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return ValidationResult.Success;

        var firstLetter = value.ToString().Substring(0, 1);
        if (firstLetter != firstLetter.ToUpper())
            return new ValidationResult("First letter must be upper case");

        return base.IsValid(value, validationContext);
    }
}