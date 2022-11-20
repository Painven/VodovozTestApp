using System.ComponentModel.DataAnnotations;

namespace VodovozTestApp.DataAccess;

public class OrderTag
{
    [Key]
    public int tag_id { get; set; }
    [Required]
    public string name { get; set; }
}