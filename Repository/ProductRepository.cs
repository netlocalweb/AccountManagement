using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class ProductRepository : RepositoryBase<Products>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
        //Merr te gjitha produketet me kategorine dhe i rendit sipas emrit 
        public async Task<IEnumerable<Products>> GetAllProductsAsync(bool trackchanges) =>
            await FindAll(trackchanges)
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .ToListAsync();

        //Merr nje produkt sipas emrit
        public async Task<Products?> GetProductByNameAsync(string name, bool trackchanges)
        {
          return  await FindByCondition(p => p.Name.ToLower() == name.ToLower(),trackchanges)
                .FirstOrDefaultAsync();
        }
        //Merr nje prd sipas id dhe me kategorine e tij
        public async Task<Products?> GetProductsByIdAsync(int id, bool trackchanges) =>
            await FindByCondition(p => p.Id.Equals(id), trackchanges)
            .Include(p => p.Category)
            .SingleOrDefaultAsync();

    }
}
