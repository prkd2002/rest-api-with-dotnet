using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace api.Controllers
{
     [Route("api/comment")]
    [ApiController]
    public class CommentController:ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ApplicationDBContext _context;
        public CommentController(ApplicationDBContext applicationDBContext, ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _context = applicationDBContext;
            
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commentsDto = comments.Select(com => com.ToCommentDto());
            return Ok(commentsDto);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var commentModel = await _commentRepository.GetByIdAsync(id);
            if(commentModel == null){
                return NotFound();
            }
            var commentDto  = commentModel.ToCommentDto();
            return Ok(commentDto);


        }

        [HttpPost("createComment")]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequest createRequestComment)
        {
            var commentModel = createRequestComment.toCommentFromCreateDto();
            await _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id},commentModel.ToCommentDto() );


        }


        [HttpPut("updateComment/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest updateCommentRequest){
            var commentModel = await _commentRepository.GetByIdAsync(id);
            if(commentModel == null){
                return NotFound();
            }
            await _commentRepository.UpdateCommentAsync(id, updateCommentRequest);
            return Ok(commentModel.ToCommentDto());

        }


        [HttpDelete("deleteComment/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _commentRepository.GetByIdAsync(id);
            if(commentModel == null){
                return NotFound();
            }
            await _commentRepository.RemoveCommentAsync(id);
            return NoContent();
        }


        
    }
}