using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.DTOs.Comments;
using TaskFlow.Api.Exceptions;
using TaskFlow.Api.Models;
using TaskFlow.Api.Services.Interfaces;

namespace TaskFlow.Api.Services;

public class CommentService : ICommentService
{
    private readonly TaskFlowDbContext _context;

    public CommentService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CommentResponseDto>> GetAllByTaskIdAsync(int taskId, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task is null)
        {
            throw new KeyNotFoundException("Task not found.");
        }

        EnsureOwnership(task.Project.UserId, userId);

        return await _context.TaskComments
            .AsNoTracking()
            .Where(c => c.TaskItemId == taskId)
            .Select(comment => new CommentResponseDto
            {
                Id = comment.Id,
                Content = comment.Content,
                TaskItemId = comment.TaskItemId
            })
            .ToListAsync();
    }

    public async Task<CommentResponseDto> GetByIdAsync(int taskId, int commentId, int userId)
    {
        var comment = await _context.TaskComments
            .Include(c => c.TaskItem)
            .ThenInclude(t => t.Project)
            .FirstOrDefaultAsync(c => c.Id == commentId && c.TaskItemId == taskId);

        if (comment is null)
        {
            throw new KeyNotFoundException("Comment not found.");
        }

        EnsureOwnership(comment.TaskItem.Project.UserId, userId);

        return new CommentResponseDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskItemId = comment.TaskItemId
        };
    }

    public async Task<CommentResponseDto> CreateAsync(int taskId, CreateCommentDto dto, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == taskId);

        if (task is null)
        {
            throw new KeyNotFoundException("Task not found.");
        }

        EnsureOwnership(task.Project.UserId, userId);

        var comment = new TaskComment
        {
            Content = dto.Content,
            TaskItemId = taskId
        };

        _context.TaskComments.Add(comment);
        await _context.SaveChangesAsync();

        return new CommentResponseDto
        {
            Id = comment.Id,
            Content = comment.Content,
            TaskItemId = comment.TaskItemId
        };
    }

    public async Task UpdateAsync(int taskId, int commentId, UpdateCommentDto dto, int userId)
    {
        var comment = await _context.TaskComments
            .Include(c => c.TaskItem)
            .ThenInclude(t => t.Project)
            .FirstOrDefaultAsync(c => c.Id == commentId && c.TaskItemId == taskId);

        if (comment is null)
        {
            throw new KeyNotFoundException("Comment not found.");
        }

        EnsureOwnership(comment.TaskItem.Project.UserId, userId);

        comment.Content = dto.Content;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int taskId, int commentId, int userId)
    {
        var comment = await _context.TaskComments
            .Include(c => c.TaskItem)
            .ThenInclude(t => t.Project)
            .FirstOrDefaultAsync(c => c.Id == commentId && c.TaskItemId == taskId);

        if (comment is null)
        {
            throw new KeyNotFoundException("Comment not found.");
        }

        EnsureOwnership(comment.TaskItem.Project.UserId, userId);

        _context.TaskComments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    private static void EnsureOwnership(int ownerUserId, int currentUserId)
    {
        if (ownerUserId != currentUserId)
        {
            throw new ForbiddenAccessException("You are not allowed to access comments for this task.");
        }
    }
}