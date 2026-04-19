using TaskFlow.Api.DTOs.Projects;

namespace TaskFlow.Api.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseDto>> GetAllAsync();
    Task<ProjectResponseDto> GetByIdAsync(int id);
    Task<ProjectResponseDto> CreateAsync(CreateProjectDto dto, int userId);
    Task UpdateAsync(int id, UpdateProjectDto dto, int userId);
    Task DeleteAsync(int id, int userId);
}