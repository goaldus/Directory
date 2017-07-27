using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Directory.DAL.ValidationRules
{
    public class IdentificationNumber : ValidationAttribute
    {
        public IdentificationNumber(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var regex = new Regex(@"^\d{8}$");
                var identificationNumber = value.ToString();

                if (!regex.Match(identificationNumber).Success)
                    return new ValidationResult("IČ musí mít pouze 8 číslic!");

                var controlNumber = (int)(identificationNumber[7] - '0');

                int sum = 0;
                int multiplier = 8;
                for (int i = 0; i < 7; i++, multiplier--)
                {
                    sum += (int)(identificationNumber[i] - '0') * multiplier;
                }

                int remainder = sum % 11;
                if ((11 - remainder) % 10 != controlNumber)
                {
                    var errormessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errormessage);
                }
                    
            }
            return ValidationResult.Success;
        }
    }
}