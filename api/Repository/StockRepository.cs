using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository(ApplicationDBContext context) : IStockRepository
    {
        private readonly ApplicationDBContext _context = context;

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> RemoveAsync(int id)
        {
          var stockModel = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
          if(stockModel == null){
            return null;
          }

          _context.Stock.Remove(stockModel);
          await _context.SaveChangesAsync();
          return stockModel;

        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks =   _context.Stock.Include(stock => stock.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol))
            {
                stocks = stocks.Where(stock => stock.Symbol.Contains(queryObject.Symbol));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                if(queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
                else if(queryObject.SortBy.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = queryObject.IsDescending ? stocks.OrderByDescending(stock => stock.CompanyName) : stocks.OrderBy(s => s.CompanyName);
                }
            }

            var skipNumber = (queryObject.PageNumber -1)* queryObject.PageSize;
            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }


        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stockModel = await _context.Stock.Include(stock => stock.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if(stockModel == null){
                return null;
            }
            return stockModel;
        }



        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
           var stock = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
           if(stock == null){
            return null;
           }

           stock.Symbol = stockDto.Symbol;
           stock.CompanyName = stockDto.CompanyName;
           stock.Industry = stockDto.Industry;
           stock.LastDiv = stockDto.LastDiv;
           stock.MarketCap = stockDto.MarketCap;
           stock.Purchase = stockDto.Purchase;
           
           return stock;

        }


            public async Task<bool> IsStockExists(int id)
        {
            return await _context.Stock.AnyAsync(s => s.Id == id);
        }
    }
}