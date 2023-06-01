using Domain.ModelsJWT;
using System.Text.Json.Serialization;


namespace Domain.JwtNotCreadDb;
public class GetRoleModel
{
    [JsonPropertyName("role_id")]
    public int Id { get; set; }
    public string Name { get; set; }
    public Permission[] Permission { get; set; }
}
