using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ModelsJWT;

[Table("role_permission")]
public class RolePermission
{
    [Column("role_permission_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RolePermissionId { get; set; }

    [Column("role_id")]
    public int RoleId { get; set; }
    public virtual Roles? Role { get; set; }

    [Column("permission_id")]
    public int PermissionId { get; set; }
    public virtual Permission? Permission { get; set; }
}
