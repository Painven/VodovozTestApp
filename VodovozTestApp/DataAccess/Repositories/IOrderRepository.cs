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
    private readonly IDapperDatabaseAccess database;

    public OrderRepository(IDapperDatabaseAccess database)
    {
        this.database = database;
    }

    public async Task<List<Order>> GetAll()
    {
        string sql = "SELECT * FROM employee";

        var orders = await database.GetList<Order>(sql);

        return orders;
    }
}