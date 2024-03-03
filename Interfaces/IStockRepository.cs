using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stockApi.Dtos.Stock;
using stockApi.Models;

namespace stockApi.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>>GetAllAsync();
        Task<Stock?>GetByIDAsync(int id);
        Task<Stock>CreateStockAsync(Stock stockModel);
        Task<Stock?>UpdateStockAsync(int id, UpdateStockDto updateStockDto);
        Task<Stock?>DeleteStockAsync(int id);
        
        Task<bool>IsStockExistAync(int id);
        
        
    }
}