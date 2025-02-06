using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseDiary.Model
{
    public class Adalot
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Provide Count name within 50 character")]
        [Required]
        [DisplayName("Adalot")]
        public string AdalotName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        [ValidateNever]
        [NotMapped]
        public IFormFile LogoFile { get; set; }

        public string Logo { get; set; }

        [ValidateNever]
        [NotMapped]
        public IFormFile BannerFile { get; set; }
        public string Banner { get; set; }
    }
}
