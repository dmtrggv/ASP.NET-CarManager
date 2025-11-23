using System;
using System.ComponentModel.DataAnnotations;

public class DateStartedBeforeEndDateAttribute : ValidationAttribute
{
    private readonly string _endDatePropertyName;

    public DateStartedBeforeEndDateAttribute(string endDatePropertyName)
    {
        _endDatePropertyName = endDatePropertyName;
        ErrorMessage = "Start Date must be before End Date.";
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var startDate = value as DateTime?;
        var endDateProperty = validationContext.ObjectType.GetProperty(_endDatePropertyName);
        if (endDateProperty == null)
            throw new ArgumentException("Property with this name not found");

        var endDate = endDateProperty.GetValue(validationContext.ObjectInstance) as DateTime?;

        if (startDate.HasValue && endDate.HasValue && startDate.Value > endDate.Value)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}