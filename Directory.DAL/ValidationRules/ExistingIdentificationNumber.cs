using System;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace Directory.BL.ValidationRules
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
                var identificationNumber = value.ToString();

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