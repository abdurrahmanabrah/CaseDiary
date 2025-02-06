using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Reflection;

namespace CaseDiary.Model
{
    public class Complainant
    {
        public int Id { get; set; }
        [DisplayName("Complainant Name")]
        
        public string ComplainantName { get; set; }

        public string Location { get; set; }
        //public Gender Gender { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }
        [DisplayName("Phone Number")]
        public string phoneNumber { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        public string Nationality { get; set; }

        [ValidateNever]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string? ImageUrl { get; set; }
    }
}
