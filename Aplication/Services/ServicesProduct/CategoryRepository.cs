using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using System.Linq.Expressions;

namespace Aplication.Services.ServicesProduct
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IAplicationDbContext _context;
        public CategoryRepository(IAplicationDbContext context)
        {
            _context = context;
            //Console.WriteLine();
            //Console.WriteLine("Scped ishladi Category");
            //Console.WriteLine();
        }

        public async Task<bool> CreateAsync(Categories entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Categories? categories = _context.Categories.FirstOrDefault(c => c.Id == Id);
            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<IQueryable<Categories>> GetAllAsync(Expression<Func<Categories, bool>>? expression = null)
        {
            IQueryable<Categories> query = _context.Categories;
            return Task.FromResult(query);
        }

        public Task<Categories?> GetByIdAsync(int Id)
        {
            Categories? categories = _context.Categories.FirstOrDefault(x => x.Id == Id);
            return Task.FromResult(categories);
        }

        public async Task<bool> UpdateAsync(Categories entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
