using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.DTOs.Tasks;

public class CreateTaskDto
{
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Title { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public int ProjectId { get; set; }

    public DateTime? DueDate { get; set; }
}