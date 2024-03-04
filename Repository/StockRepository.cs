using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using stockApi.Data;
using stockApi.Dtos.Stock;
using stockApi.Helpers;
using stockApi.Interfaces;
using stockApi.Models;

namespace stockApi.Repository
{
    public class StockRepository : IStockRepository
    {

        private readonly ApplicationDbContext _context;


        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<Stock> CreateStockAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
            
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
           var stockModel = await _context.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
           if(stockModel==null){
            return null;
           }
           _context.Stocks.Remove(stockModel);
           await _context.SaveChangesAsync();
           return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
          var stocks = _context.Stocks.Include(c=>c.Comments).AsQueryable();
          if(!string.IsNullOrWhiteSpace(query.CompanyName)){
            stocks = stocks.Where(s=>s.CompanyName.Contains(query.CompanyName));
          }
          if(!string.IsNullOrWhiteSpace(query.Symbol)){
            stocks = stocks.Where(s=>s.Symbol.Contains(query.Symbol));
          }

          if(!string.IsNullOrWhiteSpace(query.SortBy)){
            if(query.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase)){
                stocks = query.IsDecsending?stocks.OrderByDescending(s=>s.Symbol):stocks.OrderBy(s=>s.Symbol);
            }
            if(query.SortBy.Equals("CompanyName",StringComparison.OrdinalIgnoreCase)){
                stocks = query.IsDecsending?stocks.OrderByDescending(s=>s.CompanyName):stocks.OrderBy(s=>s.CompanyName);
            }
          }
          return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIDAsync(int id)
        {
            return await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<bool> IsStockExistAync(int id)
        {
           return await _context.Stocks.AnyAsync(s=>s.Id==id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockDto updateStockDto)
        {
             var previousStock = await _context.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
            if(previousStock==null){
                return null;
            }
             previousStock.Symbol=updateStockDto.Symbol;
            previousStock.Purchase = updateStockDto.Purchase;
            previousStock.LastDiv = updateStockDto.LastDiv;
            previousStock.MarketCap = updateStockDto.MarketCap;
            previousStock.Industry = updateStockDto.Industry;
            previousStock.CompanyName = updateStockDto.CompanyName;

            await _context.SaveChangesAsync();
            return previousStock;
        }
        
    }
}