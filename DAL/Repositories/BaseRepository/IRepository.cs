using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.BaseRepository
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> get(string id);

        public Task AddAsync(T entity);

        Task<int> saveAsync();
    }
}
