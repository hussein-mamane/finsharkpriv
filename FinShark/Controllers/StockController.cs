using FinShark.DTOs.Stock;
using FinShark.Mappers;

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

    //Time performance issues 1st execution more than 1s
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
    //Time performance issues 1st execution more than 1s
    [HttpGet]
    public IActionResult GetAllStocksNoTask()
    {
        // IEnumerable<StockDto>
        var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id:int:min(1):max(2)}")]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stocks = await this._context.Stocks.FindAsync(id);
        if (stocks != null) return Ok(stocks.ToStockDto());
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateStock([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stocks.Add(stockModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id, }, stockModel.ToStockDto());
    }
}