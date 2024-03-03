using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stockApi.Data;
using stockApi.Dtos.Stock;
using stockApi.Interfaces;
using stockApi.Mapper;

namespace stockApi.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class stockController:ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public stockController(IStockRepository stockRepository)
        {
           _stockRepository = stockRepository;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var stocks =await _stockRepository.GetAllAsync();
            var stockDto = stocks.Select(s=>s.mapToStockDto());
            return Ok(stockDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getByid([FromRoute] int id){
            var stock = await _stockRepository.GetByIDAsync(id);
            if(stock==null){
                return NotFound();
            }
                return Ok(stock.mapToStockDto());
            
        }

        [HttpPost]
        public async Task<IActionResult> createStock([FromBody] CreateStockDto createStockDto ){
            var stockModel = createStockDto.mapToStockModel();
            await _stockRepository.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(getByid), new{id=stockModel.Id},stockModel.mapToStockDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> updateStock([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto){
            var stockModel = await _stockRepository.UpdateStockAsync(id,updateStockDto);
            if(stockModel==null){
                return NotFound();
            }
            
            return Ok(stockModel.mapToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> deleteStock([FromRoute] int id){
            var stockModel =await _stockRepository.DeleteStockAsync(id);
            if(stockModel==null){
                return NotFound();
            }
            return NoContent();
          
        }

        
    }
}