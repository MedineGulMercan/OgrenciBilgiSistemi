using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Repositories
{

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Context _context;

        //injection
        public Repository(Context context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().Where(predicate);

        public async Task<T?> GetByIdAsync(Guid id)
            => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);


        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid Id)
        {
            var data = await GetByIdAsync(Id);
            if(data is not null)
            {
                _context.Set<T>().Remove(data);
                await _context.SaveChangesAsync();
            }
        }

    }
}
