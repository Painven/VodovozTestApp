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
    public Task<List<Department>> GetAll()
    {
        return Task.FromResult(new List<Department>()
        {
            new Department() { Name = "Тестовый отдел 1"},
            new Department() { Name = "Тестовый отдел 2"},
            new Department() { Name = "Тестовый отдел 3"},
            new Department() { Name = "Тестовый отдел 4"},
        });
    }
}