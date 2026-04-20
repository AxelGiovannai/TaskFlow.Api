using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Api.DTOs.Comments;
using TaskFlow.Api.Services.Interfaces;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/tasks/{taskId:int}/comments")]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentResponseDto>>> GetAll(int taskId)
    {
        var userId = GetUserId();
        var comments = await _commentService.GetAllByTaskIdAsync(taskId, userId);
        return Ok(comments);
    }

    [HttpGet("{commentId:int}")]
    public async Task<ActionResult<CommentResponseDto>> GetById(int taskId, int commentId)
    {
        var userId = GetUserId();
        var comment = await _commentService.GetByIdAsync(taskId, commentId, userId);
        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult<CommentResponseDto>> Create(int taskId, [FromBody] CreateCommentDto dto)
    {
        var userId = GetUserId();
        var createdComment = await _commentService.CreateAsync(taskId, dto, userId);

        return CreatedAtAction(
            nameof(GetById),
            new { taskId = taskId, commentId = createdComment.Id },
            createdComment
        );
    }

    [HttpPut("{commentId:int}")]
    public async Task<IActionResult> Update(int taskId, int commentId, [FromBody] UpdateCommentDto dto)
    {
        var userId = GetUserId();
        await _commentService.UpdateAsync(taskId, commentId, dto, userId);
        return NoContent();
    }

    [HttpDelete("{commentId:int}")]
    public async Task<IActionResult> Delete(int taskId, int commentId)
    {
        var userId = GetUserId();
        await _commentService.DeleteAsync(taskId, commentId, userId);
        return NoContent();
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
        {
            throw new UnauthorizedAccessException("User identifier not found in token.");
        }

        return int.Parse(userIdClaim);
    }
}