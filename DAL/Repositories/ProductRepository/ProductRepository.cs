using DAL.Entities;
using DAL.Repositories.BaseRepository;
using ESTORE.Data;


namespace DAL.Repositories.ProductRepository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        public ProductRepository(ESTOREContext eSTOREContext): base(eSTOREContext)
        {
        }

        public async Task<IEnumerable<Product>> getActiveProduct()
        {
            return await FindAsync(x => x.IsActive);
        }

        public async Task<bool> DeactivateProduct(Product product)
        {
            product.IsActive = false;
            Update(product);
            await SaveAsync();
            return true; 
        }



    }
}
