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
        string sql = @"SELECT d.department_id, d.name, d.lead_id, e.employee_id
                       FROM department d
                       LEFT JOIN employee e ON (d.department_id = e.department_id)";

        using var connection = await database.GetConnection();

        var departmentDictionary = new Dictionary<int, Department>();

        var departments = (await connection.QueryAsync<Department, Employee, Department>(sql, (department, employee) =>
        {
            Department departmentEntry;

            if (!departmentDictionary.TryGetValue(department.department_id, out departmentEntry))
            {
                departmentEntry = department;
                departmentEntry.Employees = new List<Employee>();
                departmentDictionary.Add(departmentEntry.department_id, departmentEntry);
            }

            departmentEntry.Employees.Add(employee);
            return departmentEntry;
        },
            splitOn: "employee_id"))
        .Distinct()
        .ToList();

        return departments;
    }
}