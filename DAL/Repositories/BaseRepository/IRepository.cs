using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BaseRepository
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync(string id);

        public Task AddAsync(T entity);

        public Task<int> SaveAsync();
         
        public Task<IEnumerable<T>> GetAllAsync();
         
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
         
        public void RemoveByIdAsync(T entity);
     /*   public void RemoveAllAsync();
*/
        public void Update(T entity);

     /*   public void Remove(T entity);*/

        public void RemoveRange(IEnumerable<T> entities);

    }
}
