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
    }
}
