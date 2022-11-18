using System.Collections.Generic;
using System.Threading.Tasks;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.DataAccess;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAll();
}

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDapperDatabaseAccess database;

    public EmployeeRepository(IDapperDatabaseAccess database)
    {
        this.database = database;
    }

    public async Task<List<Employee>> GetAll()
    {
        string sql = "SELECT * FROM employee";

        var employees = await database.GetList<Employee>(sql);

        return employees;
    }
}