using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VodovozTestApp.DataAccess;

public class Order
{
    [Key]
    public int OrderID { get; set; }
    public string ProductName { get; set; }
    public List<string> Tags { get; set; } = new();
}
