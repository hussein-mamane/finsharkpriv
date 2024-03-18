using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.Models;

[Table("Stocks")]
public class Stock
{
    public int Id { get; set; }
    public long MarketCap { get; set; }
    [MaxLength(4)]public string Symbol { get; set; } = string.Empty;
    [MaxLength(30)]public string CompanyName { get; set; } = string.Empty;
    [MaxLength(20)]public string Industry { get; set; } = string.Empty;
    [Column(TypeName ="decimal(18,2)")] public decimal Purchase { get; set; }
    [Column(TypeName ="decimal(18,2)")] public decimal LastDiv { get; set; }
    //Navigation Property
    public List<Comment> Comments = [];//new List<Comments> but in collection expression
}