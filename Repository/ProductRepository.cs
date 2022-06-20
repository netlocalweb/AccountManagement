using Contracts;
using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        protected RepositoryContext RepositoryContext;

        public ProductRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        //Method CREATE
        public void CreateRecord(Product product, out string ErrorMessage)
        {
            
                ErrorMessage = "Product added to database!";
                RepositoryContext.Products.Add(product);
        
        }

        
        //Method GETALL
        public IEnumerable<Product> GetAllRecords()
        {
            var testAll = RepositoryContext.Products;
            return (IEnumerable<Product>)testAll;
            
        }
        //Method GETBYID
        public Product GetRecordById(int id)
        {
            var product = RepositoryContext.Products.Where(x => x.Id == id).FirstOrDefault();
            return product;
        }

        //Method DELETE
        public void RemoveRecord(int id)
        {
            var product = RepositoryContext.Products.Where(x => x.Id == id).FirstOrDefault();
            RepositoryContext.Products.Remove(product);
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }

        //Method UPDATE
        public void UpdateRecord(int id, Product newProduct, out string ErrorMessage)
        {
            string errorMessage = string.Empty;

            var oldProduct = RepositoryContext.Products.Where(x => x.Id == id).FirstOrDefault();

            oldProduct.Name = newProduct.Name;
            oldProduct.ShortDescription = newProduct.ShortDescription;
            oldProduct.LongDescription = newProduct.LongDescription;
            oldProduct.Price = newProduct.Price;
            oldProduct.DateModified = DateTime.Now;
            ErrorMessage = "Product updated sucefully!";
        }

        public void UploadImage(int id, string Image)
        {
            var product = RepositoryContext.Products.Where(x => x.Id == id).FirstOrDefault();
            product.Image = Image;
        }

        
        

    }
}
