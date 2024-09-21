using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository ;
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult>  GetAll(){
            var stocks = await _stockRepository.GetAllAsync();
             var stockDto =    stocks.Select(s => s.ToSTockDto());
            return Ok(stocks);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await  _stockRepository.GetByIdAsync(id);
            if(stock == null){
                return NotFound();
            }
            return Ok(stock.ToSTockDto());
            
        }

        [HttpPost("createStock")]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            var stockmodel = stockDto.toStockFromCreateDto();
            await _stockRepository.CreateAsync(stockmodel);
            return CreatedAtAction(nameof(GetById), new {id = stockmodel.Id}, stockmodel.ToSTockDto());

        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            var Stock = await _stockRepository.UpdateAsync(id,updateStockRequestDto);
            if(Stock == null){
                return NotFound();
            }
            return Ok(Stock.ToSTockDto());

            
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var Stock = await _stockRepository.RemoveAsync(id);
            if(Stock == null){
                return NotFound();
            }
        
            //return Ok("The Stock With " + id + "has been deleted");
            return NoContent();
        }
        


    }
    
}