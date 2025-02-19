using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace CaseDiary.Model
{
    public class CaseDiaryContext : DbContext
    {
        public CaseDiaryContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Adalot> Adalot { get; set; }
        public DbSet<CaseDetails> CaseDetails { get; set; }
        public DbSet<CaseMaster> CaseMaster { get; set; }
        public DbSet<CaseSource> CaseSource { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Complainant> Complainant { get; set; }
        public DbSet<Court> Court { get; set; }
        public DbSet<Badi> Badi { get; set; }


    }
}




//public class CaseDiaryCOntext : DbContext
//{
//    public CaseDiaryCOntext() : base("diarycon")
//    {

//    }
//    public DbSet<Adalot> Adalot { get; set; }
//    public DbSet<CaseDetails> CaseDetails { get; set; }
//    public DbSet<CAseMaster> CAseMasters { get; set; }
//    public DbSet<CaseSource> CaseSources { get; set; }
//    public DbSet<Section> Sections { get; set; }


//}
//public class Adalot
//{

//    [Key]//pk
//    public int ID { get; set; }
//    [StringLength(50, ErrorMessage = "Provide Court name within 50 characters")]
//    [Required]
//    public string Name { get; set; }

//    public string Description { get; set; }
//}

//public class CaseSource
//{
//    public int ID { get; set; }
//    [MinLength(5)]
//    [MaxLength(25)]
//    public string Name { get; set; }
//}
//public class Section
//{
//    public int ID { get; set; }
//    public string Name { get; set; }
//    public string Description { get; set; }
//}
//public class CAseMaster
//{
//    public int ID { get; set; }
//    public string CaseNumber { get; set; }
//    public string Description { get; set; }
//    [DataType(DataType.Date)]
//    //[DisplayFormat(formate)]
//    [Display(Name = "Case Date ")]
//    public DateTime CaseDate { get; set; }
//    [ForeignKey("Section")]
//    [DisplayName("Section")]
//    public int SectionId { get; set; }
//    [ForeignKey("Adalot")]
//    public int AdalotId { get; set; }
//    public Section Section { get; set; }
//    public Adalot Adalot { get; set; }

//}
//public class CaseDetails
//{
//    public int Id { get; set; }

//    public DateTime HieringDate { get; set; }
//    public DateTime NextHieringDate { get; set; }
//    public string Hiering { get; set; }
//    public string Comments { get; set; }
//    [ForeignKey("CAseMaster")]
//    public int CaseId { get; set; }
//    public CAseMaster CAseMaster { get; set; }
//}