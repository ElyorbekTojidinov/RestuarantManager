using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.ModelsJWT;

[Table("user_refresh_tokens")]
public class UserRefreshToken
{
    [Column("refresh_token_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? RefreshToken { get; set; }
    public bool IsActive { get; set; }
    public DateTime Expiretime { get; set; }
}
