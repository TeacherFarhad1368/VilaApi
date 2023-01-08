using System.ComponentModel.DataAnnotations;
using Vila.WebApi.Dtos;
using Vila.WebApi.Utility;

namespace Vila.WebApi.ModelValidation
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var vila = (VilaDto)validationContext.ObjectInstance;
            try
            {
                var date = vila.BuildDate.ToEnglishDateTime();
                if (date > DateTime.Now)
                    return new ValidationResult("تاریخ ساخت باید در گذشته باشد .");

                return ValidationResult.Success;
            }
            catch 
            {
                return new ValidationResult("(ماه و روز 2 رقمی باشد .)تاریخ ساخت باید در فرمت 1396/01/12 باشد .");
            }
        }
    }
}
