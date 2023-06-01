using Aplication.Abstractions;
using Aplication.Interfaces.InterfacesProducts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services.ServicesProduct
{
    public class ProductRepository : IProductRepository
    {
        private readonly IAplicationDbContext _dbContext;

        public ProductRepository(IAplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> CreateAsync(Products entity)
        {
            _dbContext.Products.Add(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Products? product = _dbContext.Products.FirstOrDefault(x => x.ProductId == Id);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public Task<IQueryable<Products>> GetAllAsync(Expression<Func<Products, bool>>? expression = null)
        {
            IQueryable<Products> query = _dbContext.Products;
            return Task.FromResult(query);
        }

        public Task<Products> GetByIdAsync(int Id)
        {
            Products? products = _dbContext.Products.FirstOrDefault(x
                => x.ProductId == Id);
            return Task.FromResult(products);
        }

        public async Task<bool> UpdateAsync(Products entity)
        {
            _dbContext.Products.Update(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
