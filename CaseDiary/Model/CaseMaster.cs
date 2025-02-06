using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CaseDiary.Model
{
    public class CaseMaster
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(20)]
        public string CaseNumber { get; set; } = default!;

        public string Description { get; set; }

        [Required, ForeignKey(nameof(Section))]
        public int SectionId { get; set; } = default!;
        public Section Section { get; set; } = default!;

        [ForeignKey("Badi")]
        public int BadiId { get; set; }
        public Badi Badi { get; set; }

        [ForeignKey("Complainant")]
        public int ComplainantId { get; set; }
        public Complainant Complainant { get; set; }

        [Required, DataType(DataType.Date)]
        [Display(Name = "Case Date")]
        public DateTime CaseDate { get; set; }
        [Required, ForeignKey(nameof(CaseSource))]
        [DisplayName("Case Source")]
        public int CaseSourceId { get; set; }
        public CaseSource CaseSource { get; set; }
        [Required, StringLength(1000)]
        public string Details { get; set; } = default!;
        public bool Status { get; set; }

        [Required, ForeignKey(nameof(Court))]
        [DisplayName("Court")]
        public int CourtId { get; set; }
        public Court Court { get; set; } = default!;

        [DisplayName("Adalot")]
        [ForeignKey(nameof(Adalot))]
        public int AdalotId { get; set; }
        public Adalot Adalot { get; set; }

        
        public ICollection<CaseDetails> CaseDetails { get; set; } = new List<CaseDetails>();
    }
}
