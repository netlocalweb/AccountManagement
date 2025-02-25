using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public ProductRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Create(Product product)
        {
            _repositoryContext.Products.Add(product);
            _repositoryContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _repositoryContext.Products.Find(id);
            if (product != null)
            {
                _repositoryContext.Products.Remove(product);
                _repositoryContext.SaveChanges();
            }
        }

        public ICollection<Product> FindAll()
        {
            return _repositoryContext.Products.ToList();
        }

        public Product FindById(int id)
        {
            return _repositoryContext.Products.Find(id);
        }

        public bool Update(Product entity)
        {
            var existingProduct = _repositoryContext.Products.Find(entity.Id);

            // Update the DateModified
            existingProduct.DateModified = DateTime.Now;

            // Update the other properties
            existingProduct.Name = entity.Name;
            existingProduct.ShortDescription = entity.ShortDescription;
            existingProduct.LongDescription = entity.LongDescription;
            existingProduct.CategoryId = entity.CategoryId;
            existingProduct.Price = entity.Price;
            existingProduct.Image = entity.Image;


            // Save the changes
            _repositoryContext.SaveChanges();
            return true;
        }

    }
}
