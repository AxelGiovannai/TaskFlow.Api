namespace TaskFlow.Api.DTOs.Projects;

public class UpdateProjectDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}