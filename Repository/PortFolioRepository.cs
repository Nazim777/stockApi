using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using stockApi.Data;
using stockApi.Interfaces;
using stockApi.Models;

namespace stockApi.Repository
{
    public class PortFolioRepository : IPortFolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortFolioRepository(ApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<List<Stock>> GetUserPortFolio(AppUser user)
        {
            return await _context.Portfolios.Where(u=>u.AppUserId==user.Id).Select(stock=>new Stock{
                 Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
           
        }
    }
}