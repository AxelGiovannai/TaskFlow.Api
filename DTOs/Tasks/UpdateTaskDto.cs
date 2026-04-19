using TaskFlow.Api.Models.Enums;

namespace TaskFlow.Api.DTOs.Tasks;

public class UpdateTaskDto
{
    public string Title { get; set; } = null!;

    public TaskItemStatus Status { get; set; }

    public DateTime? DueDate { get; set; }
}