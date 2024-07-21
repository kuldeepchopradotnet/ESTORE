using DAL.Entities;
using DAL.Repositories.BaseRepository;
using ESTORE.Data;


namespace DAL.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {

        public OrderRepository(ESTOREContext eSTOREContext): base(eSTOREContext)
        {
        }



    }
}
