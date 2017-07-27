using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml;

namespace Directory.DAL.ValidationRules
{
    public class ExistingIdentificationNumber : ValidationAttribute
    {
        public ExistingIdentificationNumber(string errorMessage)
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

                var xmldoc = new XmlDocument();
                xmldoc.Load("http://wwwinfo.mfcr.cz/cgi-bin/ares/darv_std.cgi?ico=" + identificationNumber);


                XmlNodeList nodeList = xmldoc.GetElementsByTagName("are:Pocet_zaznamu");

                int entryNumber = 0;
                foreach (XmlNode node in nodeList)
                {
                    entryNumber = Convert.ToInt32(node.InnerText);
                }

                if (entryNumber == 0)
                {
                    var errormessage = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(errormessage);
                }

            }
            return ValidationResult.Success;
        }
    }
}