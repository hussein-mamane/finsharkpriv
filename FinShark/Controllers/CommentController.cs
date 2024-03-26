using FinShark.Interfaces;
using FinShark.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers;

[ApiController]
[Route("api/comment")]
public class CommentController:ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        var dtoComments = comments.Select(c => c.ToCommentDto());
        return Ok(dtoComments);
    }
    
    [Route("{id:int:min(1)}")]
    [HttpGet]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) return NotFound();
        var dtoComment = comment.ToCommentDto();
        return Ok(dtoComment);
    }
}