using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseDiary.Model
{
    public class CaseDetails
    {
        [Key]
        public int Id { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime CurrentHearingDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime NextHearingDate { get; set; }

        public string Hiring { get; set; }

        [Required, StringLength(500)]
        public string Comment { get; set; }
        [ForeignKey("CaseMaster")]
        public int CaseId { get; set; }
        [ValidateNever]
        public CaseMaster CaseMaster { get; set; }
    }
}
