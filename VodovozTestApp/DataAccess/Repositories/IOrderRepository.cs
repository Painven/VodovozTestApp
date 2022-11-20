using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.DataAccess;

public interface IOrderRepository
{
    Task<List<Order>> GetAll();
    Task<List<OrderTag>> GetTags();
    Task AddNew(Order order);
    Task Update(Order order);
    Task<Order> GetById(int order_id);
}

public class OrderRepository : IOrderRepository
{
    private readonly IDapperDatabaseAccess database;

    public OrderRepository(IDapperDatabaseAccess database)
    {
        this.database = database;
    }

    public async Task AddNew(Order order)
    {
        const string sql = @"INSERT INTO `order` (product_name, employee_id) VALUES 
                                                 (@product_name, @employee_id)";
        await database.Execute(sql, order);

        order.order_id = await database.GetSingle<int>("SELECT max(order_id) FROM `order`");


        await InsertTagsForOrderIfNeed(order);
    }

    public async Task Update(Order order)
    {
        const string sql = @"UPDATE `order` 
                            SET product_name = @product_name, employee_id = @employee_id
                            WHERE order_id = @order_id";
        await database.Execute(sql, order);

        const string deleteSql = "DELETE FROM `order_to_tag` WHERE order_id = @order_id";
        await database.Execute(deleteSql, new { order.order_id });
        
        await InsertTagsForOrderIfNeed(order);
    }

    private async Task InsertTagsForOrderIfNeed(Order order)
    {
        if (order.Tags.Any())
        {
            const string insertTagSql = "INSERT INTO `order_to_tag` (order_id, tag_id) VALUES (@order_id, @tag_id)";
            order.Tags.ToList().ForEach(async t =>
            {
                await database.Execute(insertTagSql, new { order.order_id, t.tag_id });
            });
        }
    }

    public async Task<List<Order>> GetAll()
    {
        const string sql = @"SELECT o.order_id, o.product_name, o.employee_id, ott.tag_id, ot.name
                        FROM `order` o
                        LEFT JOIN `order_to_tag` ott ON (o.order_id = ott.order_id) 
                        LEFT JOIN `order_tag` ot ON (ott.tag_id = ot.tag_id)
                        ORDER BY o.order_id DESC";

        using var connection = await database.GetConnection();

        var ordersDic = new Dictionary<int, Order>();

        var orders = (await connection.QueryAsync<Order, OrderTag, Order>(sql, (order, tag) =>
        {
            Order orderEntry;

            if (!ordersDic.TryGetValue(order.order_id, out orderEntry))
            {
                orderEntry = order;
                orderEntry.Tags = new List<OrderTag>();
                ordersDic.Add(orderEntry.order_id, orderEntry);
            }
            if(tag != null)
                orderEntry.Tags.Add(tag);

            return orderEntry;
        },
            splitOn: "tag_id"))
        .Distinct()
        .ToList();

        return orders;
    }

    public async Task<List<OrderTag>> GetTags()
    {
        const string sql = "SELECT * FROM `order_tag`";

        var tags = await database.GetList<OrderTag>(sql);

        return tags;
    }

    public async Task<Order> GetById(int order_id)
    {
        const string sqlOrder = "SELECT * FROM `order` WHERE order_id = @order_id";
        var order = await database.GetSingle<Order>(sqlOrder, new { order_id });

        const string sqlTag = @"SELECT * 
                                FROM `order_to_tag` ott 
                                JOIN `order_tag` ot ON (ott.tag_id = ot.tag_id) 
                                WHERE ott.order_id = @order_id";
        order.Tags = await database.GetList<OrderTag>(sqlTag, new { order_id });

        const string managerSql = @"SELECT * 
                                    FROM employee 
                                    WHERE employee_id = @employee_id";
        order.Manager = await database.GetSingle<Employee>(managerSql, new { employee_id = order.employee_id });

        return order;
    }
}