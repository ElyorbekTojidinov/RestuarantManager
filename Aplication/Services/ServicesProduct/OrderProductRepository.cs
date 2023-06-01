using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesProduct
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly IAplicationDbContext _dbContext;
        public OrderProductRepository(IAplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }

        public async Task<bool> CreateAsync(OrderProduct entity)
        {
            _dbContext.OrderProduct.Add(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            OrderProduct? query = _dbContext.OrderProduct.FirstOrDefault(x => x.Id == Id);
            _dbContext.OrderProduct.Remove(query);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IQueryable<OrderProduct>> GetAllAsync(Expression<Func<OrderProduct, bool>>? expression = null)
        {
            IQueryable<OrderProduct> query = _dbContext.OrderProduct;
            return Task.FromResult(query);
        }

        public Task<OrderProduct> GetByIdAsync(int Id)
        {
            OrderProduct? res = _dbContext.OrderProduct.FirstOrDefault(x => x.Id == Id);
            return Task.FromResult(res);
        }

        public async Task<bool> UpdateAsync(OrderProduct entity)
        {
            _dbContext.OrderProduct.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
