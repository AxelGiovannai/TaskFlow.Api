using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.DTOs.Projects;
using TaskFlow.Api.Models;
using TaskFlow.Api.Services.Interfaces;
using TaskFlow.Api.Exceptions;

namespace TaskFlow.Api.Services;

public class ProjectService : IProjectService
{
    private readonly TaskFlowDbContext _context;

    public ProjectService(TaskFlowDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectResponseDto>> GetAllAsync()
    {
        return await _context.Projects
            .AsNoTracking()
            .Select(project => new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreationDate = project.CreationDate,
                UserId = project.UserId
            })
            .ToListAsync();
    }

    public async Task<ProjectResponseDto> GetByIdAsync(int id)
    {
        var project = await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreationDate = project.CreationDate,
            UserId = project.UserId
        };
    }

    public async Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto, int userId)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreationDate = project.CreationDate,
            UserId = project.UserId
        };
    }

    public async Task UpdateAsync(int id, UpdateProjectDto dto, int userId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        if (project.UserId != userId)
        {
            throw new ForbiddenAccessException("You are not allowed to update this project.");
        }

        project.Name = dto.Name;
        project.Description = dto.Description;

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, int userId)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project is null)
        {
            throw new KeyNotFoundException("Project not found.");
        }

        if (project.UserId != userId)
        {
            throw new ForbiddenAccessException("You are not allowed to delete this project.");
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }
}