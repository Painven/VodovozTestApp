using System.Collections.Generic;
using System.Threading.Tasks;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.DataAccess;

public interface IOrderRepository
{
    Task<List<Order>> GetAll();
}

public class OrderRepository : IOrderRepository
{
    public Task<List<Order>> GetAll()
    {
        throw new System.NotImplementedException();
    }
}