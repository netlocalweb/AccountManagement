using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        //Merr te gjitha entitetet
        IQueryable<T> FindAll(bool trackChanges);
        //Merr entitetet sipas kushtit 
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        //Shton nje entitet
        void Create(T entity);
        //Perditeson nje entitet
        void Update(T entity);
        //Fshin nje entitet
        void Delete(T entity);
    }
}
