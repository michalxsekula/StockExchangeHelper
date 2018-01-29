using System;
using System.ComponentModel.DataAnnotations;

namespace StockExchangeHelper.Models
{
    public class DateLessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateLessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime currentValue;
            DateTime comparisonValue;
            var maxTimeDifference = TimeSpan.FromDays(93);
            ErrorMessage = "First Date is bigger than second Date.";

            try
            {
                currentValue = (DateTime) value;
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid date format.");
            }

            try
            {
                var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
                comparisonValue = (DateTime) property.GetValue(validationContext.ObjectInstance);
            }
            catch (Exception)
            {
                return new ValidationResult("Invalid property data to compare.");
            }

            if (currentValue + maxTimeDifference < comparisonValue)
                return new ValidationResult(
                    $"Max difference between start and end date is {maxTimeDifference.Days} days");

            return currentValue > comparisonValue
                ? new ValidationResult("Start Date is bigger than End Date.")
                : ValidationResult.Success;
        }
    }
}