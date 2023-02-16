using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.DataAccess;

public interface IDepartmentRepository
{
    Task<List<Department>> GetAll();
    Task AddOrUpdate(Department department);
    Task Delete(int departmentId);
    Task<Department> GetById(int department_id);
}

public class DepartmentRepository : IDepartmentRepository
{
    private readonly IDapperDatabaseAccess database;

    public DepartmentRepository(IDapperDatabaseAccess database)
    {
        this.database = database;
    }

    public async Task AddOrUpdate(Department department)
    {
        string sql = @"INSERT INTO department (department_id, name, lead_id) VALUES 
                                             (@department_id, @name, @lead_id) 
                       ON DUPLICATE KEY UPDATE name = VALUES(name), lead_id = VALUES(lead_id)";

        await database.Execute(sql, department);
    }

    public async Task Delete(int departmentId)
    {
        string sql = "DELETE FROM department WHERE department_id = @departmentId";
        await database.Execute(sql, new { departmentId });
    }

    public async Task<List<Department>> GetAll()
    {
        string sql = @"SELECT * FROM department d";
        try
        {
            var departments = await database.GetList<Department>(sql);

            return departments;
        }
        catch
        {

        }
        return Enumerable.Empty<Department>().ToList();
    }

    public async Task<Department> GetById(int department_id)
    {
        const string departmentSql = @"SELECT * FROM department WHERE department_id = @department_id";
        Department department = await database.GetSingle<Department>(departmentSql, new { department_id });

        string leaderSql = "SELECT * FROM employee WHERE employee_id = @employee_id";
        department.Leader = await database.GetSingle<Employee>(leaderSql, new { employee_id = department.lead_id });

        string employeeSql = "SELECT * FROM employee WHERE department_id = @department_id";
        department.Employees = await database.GetList<Employee>(employeeSql, new { department_id });

        return department;
    }
}