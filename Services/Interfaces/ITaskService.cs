using TaskFlow.Api.DTOs.Tasks;

namespace TaskFlow.Api.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponseDto>> GetAllAsync();
    Task<TaskResponseDto> GetByIdAsync(int id);
    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId);
    Task UpdateAsync(int id, UpdateTaskDto dto, int userId);
    Task DeleteAsync(int id, int userId);
}