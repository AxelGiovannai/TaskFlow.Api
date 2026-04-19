namespace TaskFlow.Api.DTOs.Projects;

public class CreateProjectDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}