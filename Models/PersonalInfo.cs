using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ErxTest.Models
{
    public class PersonalInfo
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Home Address")]
        public string AddressHome { get; set; }

        [Display(Name = "Work Address")]
        public string AddressWork { get; set; }

        [Display(Name = "Business Type")]
        public int BusinessTypeId { get; set; }

        public string Occupation { get; set; }
    }
}
