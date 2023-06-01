using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesJWT;
using Domain.ModelsJWT;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesJwt
{
    public class PermissionRoleRepository : IPermissionRoleRepository
    {
        private readonly IAplicationDbContext _context;

        public PermissionRoleRepository(IAplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(RolePermission entity)
        {
            _context.RolePermissions.Add(entity);
            int res = await _context.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            RolePermission? rolePermission = _context.RolePermissions.FirstOrDefault(x=> x.RolePermissionId == id);
            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<RolePermission>> GetAllAsync(Expression<Func<RolePermission, bool>>? expression = null)
        {
            var rolePermissions = await _context.RolePermissions.ToListAsync();
            return rolePermissions.AsQueryable();
        }

        public async Task<RolePermission?> GetAsync(Expression<Func<RolePermission, bool>> expression)
        {
            RolePermission? rolePermission = _context.RolePermissions.FirstOrDefault(expression);
            return rolePermission;
        }

        public async Task<bool> UpdateAsync(RolePermission entity)
        {
            RolePermission? rolePermission =await _context.RolePermissions.FirstOrDefaultAsync(x=>x.RolePermissionId == entity.RolePermissionId);
            if (rolePermission != null)
            {
                _context.RolePermissions.Update(rolePermission);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}
