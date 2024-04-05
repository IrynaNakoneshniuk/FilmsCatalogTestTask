using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsCatalogTestTask.Data.Models
{
    [Table("film_categories")]
    public class FilmCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey(nameof(Film))]
        [Column("film_id")]
        public int FilmId { get; set; }
        public required Film Film { get; set; }

        [ForeignKey(nameof(Category))]
        [Column("category_id")]
        public int CategoryId { get; set; }
        public required Category Category { get; set; }
    }
}