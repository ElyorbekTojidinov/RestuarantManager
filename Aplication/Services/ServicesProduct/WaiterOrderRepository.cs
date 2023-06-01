using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesProduct;

public class WaiterOrderRepository : IWaiterOrderRepository
{
    private readonly IAplicationDbContext _dbContext;
    public WaiterOrderRepository(IAplicationDbContext context)
    {
        _dbContext = context;
    }
    public async Task<bool> CreateAsync(WaiterOrder entity)
    {
        _dbContext.WaiterOrder.Add(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        WaiterOrder? res = _dbContext.WaiterOrder.FirstOrDefault(x => x.Id == Id);
        _dbContext.WaiterOrder.Remove(res);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<IQueryable<WaiterOrder>> GetAllAsync(Expression<Func<WaiterOrder, bool>>? expression = null)
    {
        IQueryable<WaiterOrder> query = _dbContext.WaiterOrder;
        return Task.FromResult(query);
    }

    public Task<WaiterOrder> GetByIdAsync(int Id)
    {
        WaiterOrder? query = _dbContext.WaiterOrder.FirstOrDefault(x => x.Id == Id);
        return Task.FromResult(query);
    }

    public async Task<bool> UpdateAsync(WaiterOrder entity)
    {
        _dbContext.WaiterOrder.Update(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
