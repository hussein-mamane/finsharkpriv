using System.ComponentModel.DataAnnotations;

namespace FinShark.DTOs.Stock;

public class CreateStockRequestDto
{
    public long MarketCap { get; set; }
    [MaxLength(4)]public string Symbol { get; set; } = string.Empty;
    [MaxLength(30)]public string CompanyName { get; set; } = string.Empty;
    [MaxLength(20)]public string Industry { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
}