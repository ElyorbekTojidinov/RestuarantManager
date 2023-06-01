using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    [Table("waiter_order")]
    public class WaiterOrder
    {
        [Column("id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("waiter_id")]
        public int WaiterId { get; set; }
        public virtual Waiter? Waiter { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }
        public virtual Orders? Orders { get; set; }

       
        
    }
}
