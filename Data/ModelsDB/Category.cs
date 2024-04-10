using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalogTestTask.Data.Models
{
    [Table("categories")]
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(200)")]
        public required string Name { get; set; }

        [ForeignKey(nameof(ParentCategory))]
        [Column("parent_category_id ")]
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }

        public ICollection<FilmCategory> FilmCategories { get; set; }=new List<FilmCategory>();

        [NotMapped]
        public int NestedLevel { get; set; }
    }
}