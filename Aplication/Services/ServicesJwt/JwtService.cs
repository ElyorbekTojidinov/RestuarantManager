using Aplication.Interfaces.InterfacesJWT;
using Domain.JwtNotCreadDb;
using Domain.ModelsJWT;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aplication.Services.ServicesJwt;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public JwtService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> GenerateRefreshTokenAsync(Users user)
    {
        string userJwt = await _userRepository.ComputeHashAsync(user.UserName + DateTime.UtcNow.ToString());
        return userJwt;
    }

    public async Task<Token> GenerateTokenAsync(Users user)
    {
        var User = await _userRepository.GetAsync(x => x.UserName == user.UserName);
        List<Claim> permission = new()
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
        foreach (UserRole role in User.UserRoles)
        {
            foreach (RolePermission rolePermission in role.Role.RolePermissions)
            {
                permission.Add(new Claim(ClaimTypes.Role, rolePermission.Permission.PermissionName));
            }
        }
        int min = 4;
        if (int.TryParse(_configuration["JWT:ExpiresInMinutes"], out int _min))
        {
            min = _min;
        }

        JwtSecurityToken jwtSecurityToken = new(
           issuer: _configuration["JWT:Issuer"],
           audience: _configuration["JWT:Audience"],
           claims: permission,
           expires: DateTime.UtcNow.AddMinutes(min),
           signingCredentials: new SigningCredentials(
               new SymmetricSecurityKey
               (Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),
               SecurityAlgorithms.HmacSha256)
               );

        var res = new Token()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            RefreshToken = await GenerateRefreshTokenAsync(user)
        };
        return res;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _configuration["JWT:Issuer"],
            ValidAudience = _configuration["JWT:Audience"],
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Key),
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }


        return principal;
    }
}
