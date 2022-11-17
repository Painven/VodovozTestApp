using System.ComponentModel.DataAnnotations;

namespace VodovozTestApp.DataAccess;

public class Department
{
    [Key]
    public int DepartmentID { get; set; }    
    public int? LeaderID { get; set; }
    [Required]
    public string Name { get; set; }
}
