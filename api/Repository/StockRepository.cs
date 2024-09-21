using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
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

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.ToListAsync();
        }


        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
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
    }
}