namespace FastServices.Web.Infrastructure.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) => this.IsDateTimeValid(value) && this.IsDateFormatValid(value);

        private bool IsDateTimeValid(object value)
        {
            DateTime d = Convert.ToDateTime(value).ToUniversalTime();
            return d >= DateTime.UtcNow; // Dates Greater than or equal to today are valid (true)
        }

        private bool IsDateFormatValid(object value) => DateTime.TryParse(value.ToString(), out _);
    }
}
