using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VodovozTestApp.DataAccess;

public class Department
{
    [Key]
    public int department_id { get; set; }    
    public int? lead_id { get; set; }
    [Required]
    public string name { get; set; }

    public Employee Leader { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
