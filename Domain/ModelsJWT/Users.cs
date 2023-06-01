using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ModelsJWT;

[Table("users")]
public class Users
{
    [Column("user_id")]
    [JsonPropertyName("user_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UsersId { get; set; }

    [Column("username")]
    public string UserName { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [JsonIgnore]
    public virtual ICollection<UserRole>? UserRoles { get; set; }

    [NotMapped]
    public int[] Roles { get; set; }

}
