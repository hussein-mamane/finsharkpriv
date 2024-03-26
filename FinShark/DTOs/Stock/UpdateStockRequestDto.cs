namespace FinShark.DTOs.Stock;

public class UpdateStockRequestDto
{
    // Only the things that can be updated, can add validators []
    public long MarketCap { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
}