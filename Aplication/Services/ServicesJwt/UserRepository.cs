using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesJWT;
using Domain.ModelsJWT;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;

namespace Aplication.Services.ServicesJwt
{
    public class UserRepository : IUserRepository
    {
        private readonly IAplicationDbContext _context;

        public UserRepository(IAplicationDbContext context)
        {
            _context = context;
        }

        public Task<string> ComputeHashAsync(string input)
        {
           using SHA256 sha256 = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha256.ComputeHash(inputBytes);

            StringBuilder stringBuilder = new();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("x2"));
            }
            return Task.FromResult(stringBuilder.ToString());
        }

        public async Task<bool> CreateAsync(Users entity)
        {
           entity.Password = await ComputeHashAsync(entity.Password);
            await _context.Users.AddAsync(entity);
            int re =await _context.SaveChangesAsync();
            return re > 0;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Users? user = _context.Users.FirstOrDefault(x => x.UsersId == Id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<IQueryable<Users>> GetAllAsync(Expression<Func<Users, bool>>? expression = null)
        {
            return expression == null ? await Task.FromResult(_context.Users.AsQueryable()) :
                 await Task.FromResult(_context.Users.Where(expression));
        }

        public async Task<Users?> GetAsync(Expression<Func<Users, bool>> expression)
        {
            Users? user = await _context.Users.Where(expression)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                .ThenInclude(x => x.Permission)
                .Select(x => x).FirstOrDefaultAsync();
            return user;
        }

     

        public async Task<bool> UpdateAsync(Users entity)
        {
            _context.Users.Update(entity);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }
    }
}
