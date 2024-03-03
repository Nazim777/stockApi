using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using stockApi.Dtos.Stock;
using stockApi.Models;

namespace stockApi.Mapper
{
    public static class StockMapper
    {
        public  static StockDto mapToStockDto(this Stock stockModel){
            return new StockDto{
                Id=stockModel.Id,
                Symbol=stockModel.Symbol,
                CompanyName= stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c=>c.mapToCommentDto()).ToList()
            };

        }

        public static Stock mapToStockModel(this CreateStockDto createStockDto){
            return new Stock {
                Symbol=createStockDto.Symbol,
                CompanyName= createStockDto.CompanyName,
                Purchase = createStockDto.Purchase,
                LastDiv = createStockDto.LastDiv,
                Industry = createStockDto.Industry,
                MarketCap = createStockDto.MarketCap


               
            };
        }
    }
}