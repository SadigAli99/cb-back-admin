using CB.Application.Interfaces.Repositories;
using CB.Core.Entities;
using CB.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CB.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id) => await _dbSet.Where(x => x.DeletedAt == null).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.Where(x => x.DeletedAt == null).ToListAsync();

        public async Task<bool> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            entity.DeletedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>()
                .Where(x => x.DeletedAt == null)
                .AsQueryable();
        }

        public async Task<IEnumerable<T>> GetAllWithSoftDeletes() => await _dbSet.Where(x => x.DeletedAt == null).ToListAsync();


        public async Task<bool> PushDeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await SaveChangesAsync() > 0;
        }
    }

}
