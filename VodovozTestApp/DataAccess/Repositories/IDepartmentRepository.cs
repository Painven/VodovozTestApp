using System.Collections.Generic;
using System.Threading.Tasks;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.DataAccess;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAll();
}

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IDapperDatabaseAccess database;

    public DepartmentRepository(IDapperDatabaseAccess database)
    {
        this.database = database;
    }

    public async Task<List<Department>> GetAll()
    {
        string sql = "SELECT * FROM department";

        var departments = await database.GetList<Department>(sql);

        return departments;
    }
}