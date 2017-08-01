using System;
using System.ComponentModel.DataAnnotations;

namespace Directory.BL.Models
{
    public class AddressDetailModel
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        [Display(Name = "Město")]
        public string City { get; set; }
        [Display(Name = "Ulice")]
        public string Street { get; set; }
        [Display(Name = "PSČ")]
        public string ZipCode { get; set; }

        
    }
}