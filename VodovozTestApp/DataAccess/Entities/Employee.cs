using System;
using System.ComponentModel.DataAnnotations;
using VodovozTestApp.Models;

namespace VodovozTestApp.DataAccess;

public class Employee
{
    [Key]
    public int EmployeeID { get; set; }
    public int DepartmentID { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Sex Sex { get; set; }
}
