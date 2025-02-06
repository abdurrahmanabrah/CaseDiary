using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Reflection;

namespace CaseDiary.Model
{
    public class Badi
    {
        public int Id { get; set; }
        [DisplayName("Badi Name")]
        public string BadiName { get; set; }

        public string Location { get; set; }
        //public Gender Gender { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }
        [DisplayName("Phone Number")]
        public string phoneNumber { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        public string Nationality { get; set; }

        [DisplayName("Crime Committed")]
        public string Description { get; set; }
        [DisplayName("Crime Date")]
        public DateTime CrimeDate { get; set; }

        [DisplayName("Conviction Date")]
        public DateTime ConvictionDate { get; set; }

        public string Status { get; set; } //Incarcerated, Released

        [ValidateNever]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
