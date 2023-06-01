using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("order_product")]
    public class OrderProduct
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }
        public virtual Products? Products { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }
        public virtual Orders? Orders { get; set; }

    }
}
