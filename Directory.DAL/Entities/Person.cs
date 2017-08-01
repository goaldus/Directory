using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Directory.DAL.Entities.Base.Implementation;
using Directory.DAL.ValidationRules;

namespace Directory.DAL.Entities
{
    public class Person : EntityBase
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [ValidIdentificationNumber("{0} not divisible by 11!")]
        [ExistingIdentificationNumber("{0} no record in ARES!")]
        public int? IN { get; set; }

        [RegularExpression(@"^[A-Z]{2}\d{8}(\d{1,2})?$", ErrorMessage = "TIN number has to start with state code and follow up with 8 - 10 digits eg. CZ12345678!")]
        public string TIN { get; set; }

        public virtual ICollection<Address> Adresses { get; set; } = new List<Address>();
    }
}