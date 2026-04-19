namespace TaskFlow.Api.DTOs.Projects;

public class ProjectResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreationDate { get; set; }

    public int UserId { get; set; }
}