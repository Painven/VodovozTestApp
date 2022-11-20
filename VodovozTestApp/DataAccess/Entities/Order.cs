using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VodovozTestApp.DataAccess;

public class Order
{
    [Key]
    public int order_id { get; set; }
    public int? employee_id { get; set; }
    public string product_name { get; set; }

    public ICollection<OrderTag> Tags { get; set; } = new List<OrderTag>();
    public Employee Manager { get; set; }
}
