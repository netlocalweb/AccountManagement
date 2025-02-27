using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public CategoryRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Create(Category category)
        {
            _repositoryContext.Categories.Add(category);
            _repositoryContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _repositoryContext.Categories.Find(id);
            if (category != null)
            {
                _repositoryContext.Categories.Remove(category);
                _repositoryContext.SaveChanges();
            }
        }

        public ICollection<Category> FindAll()
        {
            return _repositoryContext.Categories.ToList();
        }

        public Category FindById(int id)
        {
            return _repositoryContext.Categories.Find(id);
        }

        public bool Update(Category entity)
        {
            var existingCategory = _repositoryContext.Categories.Find(entity.Id);

            //whatever to-change properties do i shof me von
            existingCategory.DateModified = DateTime.Now;

            // Save the changes
            _repositoryContext.SaveChanges();
            return true;
        }

    }
}
