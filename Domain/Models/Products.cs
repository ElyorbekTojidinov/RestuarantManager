using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("products")]
    public class Products
    {
        [Column("product_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Column("category_id")]
        public int CategoriesId { get; set; }
        Categories? Categories { get; set; }

        [Column("product_name")]
        public required string ProductName { get; set; }

        [Column("product_price")]
        public decimal ProductPrice { get; set; }

        [Column("img")]
        public string? ProductImg { get; set; }

    }
}
