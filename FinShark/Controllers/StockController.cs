using System.Net.Mime;
using FinShark.DTOs.Stock;
using FinShark.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinShark.Controllers;
using FinShark.Data;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks ;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StockController> _logger;

    public StockController(ApplicationDbContext context, ILogger<StockController> logger)
    {
        _context = context;
        _logger = logger;
    }

    //Time performance issues 1st execution more than 500ms
    /*[HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        IEnumerable<Stock> stocks = await _context.Stocks.ToListAsync();//never null
        // IEnumerable<Stock> data = stocks.Cast<Stock>().ForEach(s=>s.ToStockDto());
        foreach (var stock in stocks)
        {
            stock.ToStockDto();
        }
        return Ok(stocks);
        // return NotFound();
    }
    */
    //Time performance issues 1st execution more than 500ms
    [HttpGet]
    public IActionResult GetAllStocksNoTask()
    {
        // IEnumerable<StockDto>
        var stocks = _context.Stocks.AsNoTracking().ToList()
            .Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id:int:min(1):max(50)}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stocks = await this._context.Stocks.FindAsync(id);
        if (stocks != null) return Ok(stocks.ToStockDto());
        return NotFound();
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stocks.Add(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id, }, stockModel.ToStockDto());
    }
    // [HttpPut("{id:int:min(1):max(50)}")]
    [HttpPut]
    [Route("{id:int:min(1)}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = _context.Stocks.Find(id);
        if (stockModel == null)
        {
            return NotFound();
        }

        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.MarketCap = updateDto.MarketCap;
        stockModel.Industry = updateDto.Industry;
        stockModel.LastDiv = updateDto.LastDiv;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.Symbol = updateDto.Symbol;
        _context.SaveChanges();
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stockModel = _context.Stocks.Find(id);
        if (stockModel == null)
            return NotFound();
        _context.Stocks.Remove(stockModel);
        _context.SaveChanges();
        // return Ok("Deletion successful");
        return NoContent();
    }
    
}