using FinShark.DTOs.Stock;
using FinShark.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Interfaces;

public interface IStockRepository
{ 
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int i,UpdateStockRequestDto stockModel);
    Task<Stock?> DeleteAsync(int id);
    
}