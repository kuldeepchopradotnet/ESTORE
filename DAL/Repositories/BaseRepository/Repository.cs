using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.BaseRepository
{
    public class Repository<T> : IRepository<T> where T : class 
    {
        protected readonly DbContext _context;
        public Repository(DbContext dbContext)
        {
            _context = dbContext;   
        }

        public async Task<T?> get(string id)
        {
           return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
             await _context.Set<T>().AddAsync(entity);
        }


        public async Task<int> saveAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
    
}
