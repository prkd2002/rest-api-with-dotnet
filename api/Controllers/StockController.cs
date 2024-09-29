using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult>  GetAll([FromQuery]QueryObject queryObject){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stocks = await _stockRepository.GetAllAsync(queryObject);
             var stockDto =    stocks.Select(s => s.ToSTockDto());
            return Ok(stocks);

        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stock = await  _stockRepository.GetByIdAsync(id);
            if(stock == null){
                return NotFound();
            }
            return Ok(stock.ToSTockDto());
            
        }

        [HttpPost("createStock")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateStockDto stockDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var stockmodel = stockDto.toStockFromCreateDto();
            await _stockRepository.CreateAsync(stockmodel);
            return CreatedAtAction(nameof(GetById), new {id = stockmodel.Id}, stockmodel.ToSTockDto());

        }

        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var Stock = await _stockRepository.UpdateAsync(id,updateStockRequestDto);
            if(Stock == null){
                return NotFound();
            }
            return Ok(Stock.ToSTockDto());

            
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var Stock = await _stockRepository.RemoveAsync(id);
            if(Stock == null){
                return NotFound();
            }
        
            //return Ok("The Stock With " + id + "has been deleted");
            return NoContent();
        }
        


    }
    
}