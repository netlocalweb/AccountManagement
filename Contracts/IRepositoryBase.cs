using System.Collections.Generic;

namespace Contracts
{
    public interface IRepositoryBase<T> where T:class
    {
        ICollection<T> FindAll();
        T FindById(int id);
        void Create(T entity);
        void Delete(int id);
        bool Update(T entity);
    }
}