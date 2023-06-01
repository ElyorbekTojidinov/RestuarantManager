using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    [Table("categories")]
    public class Categories
    {
        [Column("category_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("category_name")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [JsonIgnore]
        public virtual IList<Products> Products { get; set; }
    }
}
