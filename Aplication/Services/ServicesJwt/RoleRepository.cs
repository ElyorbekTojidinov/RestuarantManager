using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesJWT;
using Domain.ModelsJWT;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesJwt
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IAplicationDbContext _context;

        public RoleRepository(IAplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Roles entity)
        {
            _context.Roles.Add(entity);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Roles? roles = await _context.Roles.FirstOrDefaultAsync(x => x.Id == Id);
            if (roles != null)
            {
                _context.Roles.Remove(roles);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<Roles>> GetAllAsync(Expression<Func<Roles, bool>>? expression = null)
        {
            var roles = await _context.Roles.ToListAsync();
            return roles.AsQueryable();
        }

        public async Task<Roles?> GetAsync(Expression<Func<Roles, bool>> expression)
        {
            Roles? role = await _context.Roles.FirstOrDefaultAsync(expression);
            return role;
        }



        public async Task<bool> UpdateAsync(Roles entity)
        {
            Roles? role = _context.Roles.FirstOrDefault(x => x.Id == entity.Id);
            if (role != null)
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
