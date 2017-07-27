using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Directory.BL.ValidationRules;
using Directory.DAL.Entities.Base.Implementation;

namespace Directory.DAL.Entities
{
    public class Person : EntityBase
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"^\d{8}$", ErrorMessage = "IN number has to have 8 digits eg. 12345678!")]
        [IdentificationNumber("{0} not divisible by 11!")]
        [ExistingIdentificationNumber("{0} no record in ARES!")]
        public string IN { get; set; }

        [RegularExpression(@"^[A-Z]{2}\d{8}(\d{1,2})?$", ErrorMessage = "TIN number has to start with state code and follow up with 8 - 10 digits eg. CZ12345678!")]
        public string TIN { get; set; }

        public virtual ICollection<Address> Adresses { get; set; } = new List<Address>();
    }
}