using Domain.JwtNotCreadDb;
using Domain.ModelsJWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.InterfacesJWT
{
    public interface IJwtService
    {
        Task<Token> GenerateTokenAsync(Users user);
        Task<string> GenerateRefreshTokenAsync(Users user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

    }
}
