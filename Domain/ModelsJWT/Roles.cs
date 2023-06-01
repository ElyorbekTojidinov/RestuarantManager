using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ModelsJWT;

[Table("roles")]
public class Roles
{
    [Column("role_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("role_id")]
    public int Id { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<RolePermission>? RolePermissions { get; set; }
    [JsonIgnore]
    public virtual ICollection<UserRole>? UserRoles { get; set; }

    [JsonPropertyName("Permission_names")]
    [NotMapped]
    public int[]? Permissions { get; set; }

}
