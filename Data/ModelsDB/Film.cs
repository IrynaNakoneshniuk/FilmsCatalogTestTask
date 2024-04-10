using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalogTestTask.Data.Models
{
    [Table("films")]
    [Index(nameof(Director), IsUnique = false)]
    [Index(nameof(Release), IsUnique = false)]
    public class Film
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name", TypeName = "varchar(200)")]
        public required string Name { get; set; }

        [Column("director", TypeName = "varchar(200)")]
        public required string Director { get; set; }

        [DataType(DataType.Date)]
        [Column("release")]
        public DateTime Release { get; set; }

        public ICollection<FilmCategory> FilmCategories { get; set; }= new List<FilmCategory>();    
    }
}
