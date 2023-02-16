using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace VodovozTestApp.DataAccess;

public class MySqlDapperDatabaseAccess : IDapperDatabaseAccess
{
    private readonly string connectionString;

    public MySqlDapperDatabaseAccess(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task<bool> CheckConnection()
    {
        using IDbConnection connection = new MySqlConnection(connectionString);
        try
        {
            connection.Open();

            return connection.State == ConnectionState.Open;
        }
        catch
        {

        }
        return false;
    }

    public async Task Execute(string sql, object parameters = null)
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        await Task.Run(() => connection.Open());

        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task<List<T>> GetList<T>(string sql, object parameters = null)
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        await Task.Run(() => connection.Open());

        var list = await connection.QueryAsync<T>(sql, parameters);

        return list.ToList();
    }

    public async Task<T> GetSingle<T>(string sql, object parameters = null)
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        await Task.Run(() => connection.Open());

        var item = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);

        return item;
    }

    public async Task<IDbConnection> GetConnection()
    {
        using IDbConnection connection = new MySqlConnection(connectionString);

        await Task.Run(() => connection.Open());

        return connection;
    }
}
