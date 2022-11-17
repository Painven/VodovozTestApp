using System.Collections.Generic;

namespace VodovozTestApp.Models;

public class DepartmentModel
{
    public int DepartmentID { get; set; }
    public string Name { get; set; }
    public EmployeeModel Leader { get; set; }
    public List<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
}
