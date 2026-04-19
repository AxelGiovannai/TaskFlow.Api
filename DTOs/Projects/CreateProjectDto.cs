using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.DTOs.Projects;

public class CreateProjectDto
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }
}