using System.ComponentModel.DataAnnotations;

namespace CaseDiaryView.ViewModels
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string SectionName { get; set; } = default!;
        [Required, StringLength(10)]
        public string SectionNumber { get; set; } = default!;
        public string Description { get; set; }
        public ICollection<CaseMaster> Cases { get; set; } = new List<CaseMaster>();
    }
}
