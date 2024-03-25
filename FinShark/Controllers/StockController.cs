using System.Net.Mime;
using FinShark.DTOs.Stock;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.Repositories;
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
    private readonly IStockRepository _stockRepository;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StockController> _logger;

    public StockController(IStockRepository stockRepository,ApplicationDbContext context, ILogger<StockController> logger)
    {
        _stockRepository = stockRepository;
        _logger = logger;
        _context = context;
    }

    //Time performance issues 1st execution more than 500ms
    [HttpGet]
    public async Task<IActionResult> GetAllStocks()
    {
        IEnumerable<Stock> stocks = await _stockRepository.GetAllAsync();
        // IEnumerable<Stock> data = stocks.Cast<Stock>().ForEach(s=>s.ToStockDto());
        foreach (var stock in stocks)
        {
            stock.ToStockDto();
        }
        return Ok(stocks);
        
    }
    //Time performance issues 1st execution more than 500ms
    /*[HttpGet]
    public IActionResult GetAllStocksNoTask()
    {
        // IEnumerable<StockDto>
        var stocks = _context.Stocks.AsNoTracking().ToList()
            .Select(s => s.ToStockDto());
        return Ok(stocks);
    }*/

    [HttpGet("{id:int:min(1):max(50)}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStockById([FromRoute] int id)
    {
        var stocks = await _stockRepository.GetByIdAsync(id);
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
        await _stockRepository.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id, }, stockModel.ToStockDto());
    }
    // [HttpPut("{id:int:min(1):max(50)}")]
    [HttpPut]
    [Route("{id:int:min(1)}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = await _stockRepository.UpdateAsync(id, updateDto);
        if (stockModel == null)
        {
            return NotFound();
        }
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stockModel = _stockRepository.DeleteAsync(id);
        if (stockModel == null)
            return NotFound();
        // return Ok("Deletion successful");
        return NoContent();
    }
    
}