﻿using System.ComponentModel.DataAnnotations;

namespace CaseDiaryView.ViewModels
{
    public class Court
    {
        [Key]
        public int CourtId { get; set; }
        [Required, StringLength(50)]
        public string CourtName { get; set; } = default!;
        public ICollection<CaseMaster> Cases { get; set; } = new List<CaseMaster>();
    }
}
