using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToSTockDto(this Stock stockmodel)
        {
            return new StockDto 
            {
                Id = stockmodel.Id,
                Symbol = stockmodel.Symbol,
                CompanyName = stockmodel.CompanyName,
                Purchase = stockmodel.Purchase,
                LastDiv = stockmodel.LastDiv,
                Industry = stockmodel.Industry,
                MarketCap = stockmodel.MarketCap

            
            };

        } 

        public static Stock toStockFromCreateDto(this CreateStockDto createStockDto  )
        {
            return new Stock {
                Symbol = createStockDto.Symbol,
                CompanyName = createStockDto.CompanyName,
                Purchase = createStockDto.Purchase,
                LastDiv = createStockDto.LastDiv,
                Industry = createStockDto.Industry,
                MarketCap = createStockDto.MarketCap

            };
        }
    }
}