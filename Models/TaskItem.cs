using TaskFlow.Api.Models.Enums;

namespace TaskFlow.Api.Models;

public class TaskItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;

    public DateTime? DueDate { get; set; }

    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
}