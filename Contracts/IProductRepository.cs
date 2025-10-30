using Entities.Models;

namespace Contracts
{
    public interface IProductRepository : IRepositoryBase<Products>
    {
        Task<IEnumerable<Products>> GetAllProductsAsync(bool trackchanges);
        Task<Products?>GetProductsByIdAsync(int id,bool trackchanges);
        
        Task<Products?>GetProductByNameAsync(string name,bool trackchanges);

    }
}
