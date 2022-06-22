using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        protected RepositoryContext RepositoryContext;

        public CategoryRepository(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        //Method CREATE
        public void CreateRecord(Category category, out string ErrorMessage)
        {
            string errorMessage = string.Empty;
            if (DuplicateValidation(category, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
            }
            else
            {
                ErrorMessage = "Category added to database!";
                RepositoryContext.Categories.Add(category);
            }


        }

        //Method GETALL
        public IEnumerable<Category> GetAllRecords()
        {
            var testAll = RepositoryContext.Categories;
            return (IEnumerable<Category>)testAll;

        }
        //Method GETBYID
        public Category GetRecordById(int id)
        {
            var category = RepositoryContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            return category;
        }

        //Method DELETE
        public void RemoveRecord(int id, out bool check)
        {
            var category = RepositoryContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            if(category == null)
            {
                check = false;
            }
            else
            {
                check = true;
                RepositoryContext.Categories.Remove(category);
            }
            
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }

        //Method UPDATE
        public void UpdateRecord(int id, Category newCategory, out string ErrorMessage)
        {
            string errorMessage = string.Empty;

            var oldCategory = RepositoryContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            if(oldCategory == null)
            {
                ErrorMessage = "There is no category with this ID in Database";
            }
            else if (DuplicateValidation(oldCategory, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
            }
            else
            {
                ErrorMessage = "Category updated sucefully!";
                oldCategory.Code = newCategory.Code.ToUpper();
                oldCategory.Description = newCategory.Description;
                oldCategory.DateModified = DateTime.Now;
            }

        }

        //Code Duplicate Method
        public bool DuplicateValidation(Category category, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            var code = RepositoryContext.Categories.Where(x => x.Code == category.Code).FirstOrDefault();
            if (code == null)
            {
                return true;
            }
            if (code.Id == category.Id)
            {
                return true;
            }
            else if (code.Id != category.Id && code.Code == category.Code)
            {
                ErrorMessage = "Code already exists in database! Record NOT added to database.";
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
