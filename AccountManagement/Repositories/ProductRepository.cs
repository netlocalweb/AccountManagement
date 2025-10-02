using AccountManagement.API.Data;
using AccountManagement.API.Models;
using AccountManagement.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext context;
        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await context.Products
               .Include(p => p.Category)
               .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task<Product> AddAsync(Product product)
        {
           
            context.Products.Add(product);
            await context.SaveChangesAsync();
            return product;

        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var existing = await context.Products.FindAsync(product.Id);
            if (existing == null) return null;

            existing.Name = product.Name;
            existing.ShortDescription = product.ShortDescription;
            existing.LongDescription = product.LongDescription;
            existing.CategoryId = product.CategoryId;
            existing.Price = product.Price;
            existing.ImageUrl = product.ImageUrl;
            existing.DateModified = DateTime.UtcNow;

            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null) return false;

            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return true;

        }

        
    }
}
