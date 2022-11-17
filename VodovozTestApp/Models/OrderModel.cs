using System.Collections.Generic;

namespace VodovozTestApp.Models;

public class OrderModel
{
    public int OrderID { get; set; }
    public EmployeeModel Manager { get; set; }
    public string ProductName { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
}
