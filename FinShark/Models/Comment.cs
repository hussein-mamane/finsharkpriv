using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.Models;

[Table("Comments")]
public class Comment
{
    public int Id { get; set; }
    [Required] [MaxLength(200)] public string? Content { get; set; }
    [MaxLength(200)] public string Title { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    //One to many key, no fluent api
    public int? StockId { get; set; }
    //Navigation property, access other side of relation
    public Stock? Stock { get; set; }
}