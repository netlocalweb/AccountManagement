using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var objCategory = RepositoryContext.Categories.Where(x => x.Id == product.CategoryId).FirstOrDefault();
            if (objCategory == null)
            {
                ErrorMessage = "Category Id not found";
            }
            else
            {
                ErrorMessage = "Product added to database!";
                RepositoryContext.Products.Add(product);
            }


        }


        //Method GETALL
        public IEnumerable<Product> GetAllRecords(int pageNumber, int pageSize, out int totalRecords)
        {
            int skipRecords = (pageNumber - 1) * pageSize;
            totalRecords = RepositoryContext.Products.Count();
            var testAll = RepositoryContext.Products.Skip(skipRecords).Take(pageSize);
            return (IEnumerable<Product>)testAll;

        }
        //Method GETBYID
        public Product GetRecordById(int id)
        {
            var product = RepositoryContext.Products.Where(x => x.Id == id).FirstOrDefault();
            return product;
        }

        //Method DELETE
        public void RemoveRecord(int id, out bool check)
        {
            var product = RepositoryContext.Products.Where(x => x.Id == id).FirstOrDefault();
            if(product == null)
            {
                check = false;
            }
            else
            {
                check = true;
                RepositoryContext.Products.Remove(product);
            }
            
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
            var objCategory = RepositoryContext.Categories.Where(x => x.Id == newProduct.CategoryId).FirstOrDefault();
            if(oldProduct == null)
            {
                ErrorMessage = "There is no Product with this ID in Database";
            }else if (objCategory == null)
            {
                ErrorMessage = "Category Id not found";
            }
            else
            {
                oldProduct.Name = newProduct.Name;
                oldProduct.ShortDescription = newProduct.ShortDescription;
                oldProduct.LongDescription = newProduct.LongDescription;
                oldProduct.Price = newProduct.Price;
                oldProduct.DateModified = DateTime.Now;
                ErrorMessage = "Product updated sucefully!";
            }
        }

        public void UploadImage(int id, string Image)
        {
            var product = RepositoryContext.Products.Where(x => x.Id == id).FirstOrDefault();
            product.Image = Image;
        }




    }
}
