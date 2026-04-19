using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.DTOs.Tasks;
using TaskFlow.Api.Models;
using TaskFlow.Api.Models.Enums;
using TaskFlow.Api.Services.Interfaces;

namespace TaskFlow.Api.Services;

public class TaskService : ITaskService
{
    private readonly TaskFlowDbContext _context;

    public TaskService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskResponseDto>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .Select(task => new TaskResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Status = task.Status,
                DueDate = task.DueDate,
                ProjectId = task.ProjectId
            })
            .ToListAsync();
    }

    public async Task<TaskResponseDto> GetByIdAsync(int id)
    {
        var task = await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
        {
            throw new KeyNotFoundException("Task not found.");
        }

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Status = task.Status,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId
        };
    }

    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == dto.ProjectId);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        if (project.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not allowed to add a task to this project.");
        }

        var task = new TaskItem
        {
            Title = dto.Title,
            Status = TaskItemStatus.Todo,
            DueDate = dto.DueDate,
            ProjectId = dto.ProjectId
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Status = task.Status,
            DueDate = task.DueDate,
            ProjectId = task.ProjectId
        };
    }

    public async Task UpdateAsync(int id, UpdateTaskDto dto, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
        {
            throw new KeyNotFoundException("Task not found.");
        }

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == task.ProjectId);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        if (project.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not allowed to update this task.");
        }

        task.Title = dto.Title;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var task = await _context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task is null)
        {
            throw new KeyNotFoundException("Task not found.");
        }

        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == task.ProjectId);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        if (project.UserId != userId)
        {
            throw new UnauthorizedAccessException("You are not allowed to delete this task.");
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}