namespace TaskFlow.Api.Models;

public class TaskComment
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int TaskItemId { get; set; }

    public TaskItem TaskItem { get; set; } = null!;
}