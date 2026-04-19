namespace TaskFlow.Api.DTOs.Tasks;

public class CreateTaskDto
{
    public string Title { get; set; } = null!;

    public int ProjectId { get; set; }

    public DateTime? DueDate { get; set; }
}