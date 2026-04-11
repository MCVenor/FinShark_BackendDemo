using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();

            var commentDtos = comments.Select(x => x.toCommentDto());

            return Ok(commentDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Comment? comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.toCommentDto());
        }   

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
            if (!await _stockRepo.StockExistsAsync(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            Comment commentModel = commentDto.toCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id}, commentModel.toCommentDto());
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            var comment = await _commentRepo.UpdateAsync(Id, updateDto.toCommentFromUpdate());

            if (comment == null)
            { 
                return NotFound("Comment not found");
            }

            return Ok(comment.toCommentDto());

        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var commentModel = await _commentRepo.DeleteAsync(Id);

            if (commentModel == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(commentModel.toCommentDto());
        }
    }
}
