using de_training.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace de_training.Services
{
    public abstract class BaseService<TEntity> : IService<TEntity> where TEntity : class
    {
        protected readonly libraryContext _libraryContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseService(libraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            _dbSet = _libraryContext.Set<TEntity>();
        }

        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public TEntity Insert(TEntity item)
        {
            return _dbSet.Add(item).Entity;
        }
    }
}
