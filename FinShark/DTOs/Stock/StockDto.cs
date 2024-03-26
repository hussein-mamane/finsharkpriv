﻿using FinShark.DTOs.Comment;

namespace FinShark.DTOs.Stock;

public class StockDto
{
    public int Id { get; set; }
    public long MarketCap { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    public decimal LastDiv { get; set; }
    public List<CommentDto> Comments { get; set; }
    public StockDto()
    {}
}