using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VodovozTestApp.DataAccess;

namespace VodovozTestApp.DataAccess;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAll();
    Task AddOrUpdate(Employee employee);
    Task Delete(int employeeId);
}

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDapperDatabaseAccess database;

    public EmployeeRepository(IDapperDatabaseAccess database)
    {
        this.database = database;
    }

    public async Task AddOrUpdate(Employee employee)
    {
        string sql = @"INSERT INTO employee (employee_id, department_id, first_name, last_name, middle_name, sex, date_of_birth) 
                                      VALUES(@employee_id, @department_id, @first_name, @last_name, @middle_name, @sex, @date_of_birth) 
                        ON DUPLICATE KEY UPDATE 
                            department_id = VALUES(department_id),
                            first_name = VALUES(first_name),
                            last_name = VALUES(last_name),
                            middle_name = VALUES(middle_name),
                            sex = VALUES(sex),
                            date_of_birth = VALUES(date_of_birth)";

        await database.Execute(sql, employee);
    }

    public async Task Delete(int employeeId)
    {
        string sql = "DELETE FROM employee WHERE employee_id = @employeeId";
        await database.Execute(sql, new { employeeId });
    }

    public async Task<List<Employee>> GetAll()
    {
        string sql = @"SELECT e.first_name, e.last_name, e.middle_name, e.employee_id, e.date_of_birth, e.sex, d.department_id, d.name
                      FROM employee e
                      LEFT JOIN department d ON (e.department_id = d.department_id)
                      ORDER BY e.last_name, e.first_name";

        try
        {
            using var connection = await database.GetConnection();

            var employees = (await connection.QueryAsync<Employee, Department, Employee>(sql, (employee, department) =>
            {
                employee.Department = department;
                return employee;
            },
            splitOn: "department_id"))
            .ToList();

            return employees;
        }
        catch
        {

        }

        return Enumerable.Empty<Employee>().ToList();
    }
}