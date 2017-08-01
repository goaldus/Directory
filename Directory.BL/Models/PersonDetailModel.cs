using System;
using System.ComponentModel.DataAnnotations;
using Directory.DAL.ValidationRules;

namespace Directory.BL.Models
{
    public class PersonDetailModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Jméno")]
        [Required(ErrorMessage = "Jméno je povinné!")]
        public string FirstName { get; set; }

        [Display(Name = "Přijmení")]
        [Required(ErrorMessage = "Přijmení je povinné!")]
        public string LastName { get; set; }

        [Display(Name = "IČ")]
        [ValidInteger(ErrorMessage = "IČ musí mít pouze 8 číslic!")]
        [ValidIdentificationNumber("{0} není dělitelné 11!")]
        [ExistingIdentificationNumber("{0} nemá záznam v ARESu!")]
        public int? IN { get; set; }

        [Display(Name = "DIČ")]
        [RegularExpression(@"^[A-Z]{2}\d{8}(\d{1,2})?$", ErrorMessage = "DIČ musí začínat kódem státu, následovaným 8-10 číslicemi např. CZ12345678!")]
        public string TIN { get; set; }
    }
}