using MaterialDetailUsingMVC.BLL.IRepository;
using MaterialDetailUsingMVC.DLL;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace MaterialDetailUsingMVC.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly MaterialDataContext _context;
        public GenericRepository(MaterialDataContext context) => _context = context;


        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable<T>();
        }
        public IQueryable<T> GetByIdAsync(Expression<Func<T, bool>> compareExpression)
        {
            var query = _context.Set<T>().AsQueryable<T>();
            return query.Where(compareExpression);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public EntityEntry<T> Update(T entity)
        {
            return _context.Set<T>().Update(entity);
        }
    }
}
