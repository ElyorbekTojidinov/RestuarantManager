using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.ModelsJWT;

[Table("user_role")]
public class UserRole
{
    [Column("user_role_id")]
    [JsonPropertyName("user_role_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserRoleId { get; set; }

    [Column("user_id")]
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
    [JsonIgnore]
    public virtual Users? User { get; set; }

    [Column("role_id")]
    [JsonPropertyName("role_id")]
    public int RoleId { get; set; }
    [JsonIgnore]
    public virtual Roles? Role { get; set; }
}
