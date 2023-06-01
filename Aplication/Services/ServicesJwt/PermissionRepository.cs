using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesJWT;
using Domain.ModelsJWT;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesJwt;

public class PermissionRepository : IPermissionRepository
{
    private readonly IAplicationDbContext _context;

    public PermissionRepository(IAplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateAsync(Permission entity)
    {
        _context.Permissions.Add(entity);
        int res = await _context.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Permission? permission = _context.Permissions.FirstOrDefault(x=>x.PermissionId== id);
        if (permission != null)
        {
            _context.Permissions.Remove(permission);
           await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<IQueryable<Permission>> GetAllAsync(Expression<Func<Permission, bool>>? expression = null)
    {
        var permission = await _context.Permissions.ToListAsync();
        return permission.AsQueryable();
    }

    public async Task<Permission?> GetAsync(Expression<Func<Permission, bool>> expression)
    {
        Permission? permission = await _context.Permissions.FirstOrDefaultAsync(expression);
        return permission;
    }

    public async Task<bool> UpdateAsync(Permission entity)
    {
       Permission? permission = await _context.Permissions.FirstOrDefaultAsync(x=> x.PermissionId== entity.PermissionId);
        if (permission != null)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
