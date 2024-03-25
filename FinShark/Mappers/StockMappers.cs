using FinShark.DTOs.Stock;
using FinShark.Models;

namespace FinShark.Mappers;


public static class StockMappers
{
    static StockMappers()
    {
        // Empty static constructor is redundant
    }
    //extension method on the Models.Stock class, return an instance of StockDto
    public static StockDto ToStockDto(this Models.Stock stockModel)
    {
        return new StockDto
        {
            Id=stockModel.Id,
            Industry = stockModel.Industry,
            CompanyName = stockModel.CompanyName,
            LastDiv = stockModel.LastDiv,
            MarketCap = stockModel.MarketCap,
            Purchase = stockModel.Purchase,
            Symbol = stockModel.Symbol
        };
    }

    public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto)
    {
        return new Stock
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Purchase = stockDto.Purchase,
            Industry = stockDto.Industry,
            LastDiv = stockDto.LastDiv,
            MarketCap = stockDto.MarketCap
        };
    }
}