using System;

namespace VodovozTestApp.Models;

public class EmployeeModel
{
    public int EmployeeID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Sex Sex { get; set; }
    public DepartmentModel Department { get; set; }
}
