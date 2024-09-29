using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace api.Controllers
{
     [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentController( ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
           
            
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllAsync();
            var commentsDto = comments.Select(com => com.ToCommentDto());
            return Ok(commentsDto);
        }


        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var commentModel = await _commentRepository.GetByIdAsync(id);
            if(commentModel == null){
                return NotFound();
            }
            var commentDto  = commentModel.ToCommentDto();
            return Ok(commentDto);


        }

        [HttpPost("createComment/{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> Create([FromRoute] int stockId,[FromBody] CreateCommentRequest createRequestComment)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

                if(!await _stockRepository.IsStockExists(stockId))
                {
                    return BadRequest("Stock does not exist");

                }
            var commentModel = createRequestComment.ToCommentFromCreateDto(stockId);
            await _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id},commentModel.ToCommentDto() );


        }


        [HttpPut("updateComment/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest updateCommentRequest){
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepository.GetByIdAsync(id);
            if(commentModel == null){
                return NotFound();
            }
            await _commentRepository.UpdateCommentAsync(id, updateCommentRequest);
            return Ok(commentModel.ToCommentDto());

        }


        [HttpDelete("deleteComment/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _commentRepository.GetByIdAsync(id);
            if(commentModel == null){
                return NotFound();
            }
            await _commentRepository.RemoveCommentAsync(id);
            return NoContent();
        }


        
    }
}