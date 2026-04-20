using TaskFlow.Api.DTOs.Comments;

namespace TaskFlow.Api.Services.Interfaces;

public interface ICommentService
{
    Task<IEnumerable<CommentResponseDto>> GetAllByTaskIdAsync(int taskId, int userId);
    Task<CommentResponseDto> GetByIdAsync(int taskId, int commentId, int userId);
    Task<CommentResponseDto> CreateAsync(int taskId, CreateCommentDto dto, int userId);
    Task UpdateAsync(int taskId, int commentId, UpdateCommentDto dto, int userId);
    Task DeleteAsync(int taskId, int commentId, int userId);
}