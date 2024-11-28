using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MaterialDetailUsingMVC.BLL.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetByIdAsync(Expression<Func<T, bool>> compareExpression);
        void Remove(T entity);
        EntityEntry<T> Update(T entity);
    }
}