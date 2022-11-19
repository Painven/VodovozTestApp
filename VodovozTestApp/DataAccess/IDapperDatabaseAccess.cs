using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace VodovozTestApp.DataAccess;

public interface IDapperDatabaseAccess
{
    Task<List<T>> GetList<T>(string sql, object parameters = null);
    Task<T> GetSingle<T>(string sql, object parameters = null);
    Task Execute(string sql, object parameters = null);

    Task<IDbConnection> GetConnection();
}
