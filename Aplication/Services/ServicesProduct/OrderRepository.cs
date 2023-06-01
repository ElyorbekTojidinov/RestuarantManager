using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesProduct;

public class OrderRepository : IOrdersRepository
{
    private readonly IAplicationDbContext _dbContext;
    public OrderRepository(IAplicationDbContext context)
    {
        _dbContext = context;
      
    }

    public async Task<bool> CreateAsync(Orders entity)
    {
        await _dbContext.Orders.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
        Orders? orders = _dbContext.Orders.FirstOrDefault(x => x.OrderId == Id);
        _dbContext.Orders.Remove(orders);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<IQueryable<Orders>> GetAllAsync(Expression<Func<Orders, bool>>? expression = null)
    {
        IQueryable<Orders> query = _dbContext.Orders;
        return Task.FromResult(query);
    }

    public Task<Orders?> GetByIdAsync(int Id)
    {
        Orders? orders = _dbContext.Orders.FirstOrDefault(x => x.OrderId == Id);
        return Task.FromResult(orders);

    }

    public async Task<bool> UpdateAsync(Orders entity)
    {
        _dbContext.Orders.Update(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
