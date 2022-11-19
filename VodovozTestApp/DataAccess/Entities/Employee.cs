using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VodovozTestApp.Models;

namespace VodovozTestApp.DataAccess;

[Table(name: "employee")]
public class Employee
{
    [Key]
    public int employee_id { get; set; }
    public int? department_id { get; set; }
    [Required]
    public string first_name { get; set; }
    [Required]
    public string last_name { get; set; }
    public string? middle_name { get; set; }
    public DateTime? date_of_birth { get; set; }
    public Sex sex { get; set; }

    public Department Department { get; set; }
}
