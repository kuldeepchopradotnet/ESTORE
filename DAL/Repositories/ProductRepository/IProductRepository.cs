using DAL.Repositories.BaseRepository;
using DAL.Entities;

namespace DAL.Repositories.ProductRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        public Task<IEnumerable<Product>> getActiveProduct();
        public Task<bool> DeactivateProduct(Product product);
    }
}
