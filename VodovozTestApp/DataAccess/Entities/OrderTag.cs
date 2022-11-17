using System.ComponentModel.DataAnnotations;

namespace VodovozTestApp.DataAccess;

public class OrderTag
{
    [Key]
    public int OrderTagId { get; set; }
    [Required]
    public string Name { get; set; }
}