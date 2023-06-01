using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesProduct
{
    public class WaiterRepository : IWaiterRepository
    {
        private readonly IAplicationDbContext _dbContext;
        public WaiterRepository(IAplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<bool> CreateAsync(Waiter entity)
        {
            _dbContext.Waiter.Add(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Waiter? ob = _dbContext.Waiter.FirstOrDefault(x => x.waiterId == Id);
            _dbContext.Waiter.Remove(ob);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IQueryable<Waiter>> GetAllAsync(Expression<Func<Waiter, bool>>? expression = null)
        {
            IQueryable<Waiter> query = _dbContext.Waiter;
            return Task.FromResult(query);
        }

        public Task<Waiter> GetByIdAsync(int Id)
        {
            Waiter? ob = _dbContext.Waiter.FirstOrDefault(x => x.waiterId == Id);
            return Task.FromResult(ob);
        }

        public async Task<bool> UpdateAsync(Waiter entity)
        {
            _dbContext.Waiter.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
