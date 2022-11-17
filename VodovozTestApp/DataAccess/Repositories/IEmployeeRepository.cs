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
    public Task<List<Employee>> GetAll()
    {
        throw new System.NotImplementedException();
    }
}