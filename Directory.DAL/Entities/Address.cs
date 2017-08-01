using System.ComponentModel.DataAnnotations;
using Directory.DAL.Entities.Base.Implementation;

namespace Directory.DAL.Entities
{
    public class Address : EntityBase
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public virtual Person Person { get; set; }
    }
}