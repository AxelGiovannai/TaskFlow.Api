using TaskFlow.Api.DTOs.Comments;
using TaskFlow.Api.Models.Enums;

namespace TaskFlow.Api.DTOs.Tasks;

public class TaskResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public TaskItemStatus Status { get; set; }

    public DateTime? DueDate { get; set; }

    public int ProjectId { get; set; }

    public List<CommentResponseDto> Comments { get; set; } = new();
}