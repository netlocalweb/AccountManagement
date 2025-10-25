using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Products>> GetAllProductsAsync(bool trackchanges) =>
            await FindAll(trackchanges)
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .ToListAsync();

        public async Task<Products?> GetProductByNameAsync(string name, bool trackchanges)
        {
          return  await FindByCondition(p => p.Name.ToLower() == name.ToLower(),trackchanges)
                .FirstOrDefaultAsync();
        }

        public async Task<Products?> GetProductsByIdAsync(int id, bool trackchanges) =>
            await FindByCondition(p => p.Id.Equals(id), trackchanges)
            .Include(p => p.Category)
            .SingleOrDefaultAsync();


    }
}
