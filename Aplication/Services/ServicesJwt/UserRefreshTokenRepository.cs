using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesJWT;
using Domain.ModelsJWT;
using Microsoft.EntityFrameworkCore;

namespace Aplication.Services.ServicesJwt
{
    public class UserRefreshTokenRepository : IUserRefreshTokenRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IAplicationDbContext _aplicationDbContext;

        public UserRefreshTokenRepository(IAplicationDbContext aplicationDbContext, IUserRepository userRepository)
        {
            _aplicationDbContext = aplicationDbContext;
            _userRepository = userRepository;
        }

        public async Task<Domain.ModelsJWT.UserRefreshToken> AddUserRefreshTokens(Domain.ModelsJWT.UserRefreshToken user)
        {
            _aplicationDbContext.UserRefreshToken.Add(user);
            await _aplicationDbContext.SaveChangesAsync();
            return user;
            
        }

        public async Task<bool> DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var token = _aplicationDbContext.UserRefreshToken
                .FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if(token != null)
            {
                _aplicationDbContext.UserRefreshToken.Remove(token);
                await _aplicationDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Domain.ModelsJWT.UserRefreshToken> GetSavedRefreshTokens(string username, string refreshtoken)
        {
            return await _aplicationDbContext.UserRefreshToken.FirstOrDefaultAsync(x=>x.RefreshToken== refreshtoken&&x.UserName == username);
        }

        public async Task<bool> IsValidUserAsync(Users user)
        {
            string HashedPsw = await _userRepository.ComputeHashAsync(user.Password);
            var user1 = await _aplicationDbContext.Users
                .FirstOrDefaultAsync(x=>x.UserName == user.UserName && x.Password == HashedPsw);
            if(user1 != null)
            {
                return true;
            }
            return false;
        }

        public async Task<int> SaveCommit()
        {
            return await _aplicationDbContext.SaveChangesAsync();
        }

        public async Task<Domain.ModelsJWT.UserRefreshToken> UpdateUserRefreshToken(Domain.ModelsJWT.UserRefreshToken user)
        {
            var refreshToken = await _aplicationDbContext.UserRefreshToken
                .FirstOrDefaultAsync(x => x.UserName == user.UserName);
            if(refreshToken != null)
            {
                refreshToken.RefreshToken = user.RefreshToken;
                _aplicationDbContext.UserRefreshToken.Update(refreshToken);
                await _aplicationDbContext.SaveChangesAsync();
                return user;
            }
            else
            {
                await AddUserRefreshTokens(user);
                return user;
            }
        }
    }
}
