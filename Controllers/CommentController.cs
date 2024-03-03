using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using stockApi.Dtos.Comment;
using stockApi.Interfaces;
using stockApi.Mapper;
using stockApi.Repository;

namespace stockApi.Controllers
{


    [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            
        }


       [HttpGet]
        public async Task<IActionResult> GetAll(){
           var comments = await _commentRepository.GetAllAsync();
           var commentDto = comments.Select(c=>c.mapToCommentDto());
           return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var comment = await _commentRepository.GetByIdAsync(id);
            if(comment==null){
                return NotFound("Comment not found!");
            }
            return Ok(comment.mapToCommentDto());

        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> CreateComment([FromRoute] int stockId, CreateCommentDto createCommentDto){
          var stock = await _stockRepository.IsStockExistAync(stockId);
          if(!stock){
            return BadRequest("Stock does not exist!");
          }
          var commentModel = createCommentDto.mapToCreateCommentModel(stockId);
          await _commentRepository.CreateCommentAsync(commentModel);
           return CreatedAtAction(nameof(GetById), new{id=commentModel.Id},commentModel.mapToCommentDto());

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id,[FromBody] UpdateCommentDto updateCommentDto){
             var convertToCommentModel = updateCommentDto.mapToUpdateCommentModel();
             var comment =  await _commentRepository.UpdateCommentAsync(id,convertToCommentModel);
             if(comment==null){
                return NotFound("Comment not found!");
             }
             return Ok(comment.mapToCommentDto());

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id){
            var commentModel = await _commentRepository.DeleteCommentAsync(id);
            if(commentModel==null){
                return NotFound("Comment not found!");
            }
            return Ok(commentModel.mapToCommentDto());
        }        
    }
}