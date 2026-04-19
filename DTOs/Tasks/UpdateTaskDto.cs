using System.ComponentModel.DataAnnotations;
using TaskFlow.Api.Models.Enums;

namespace TaskFlow.Api.DTOs.Tasks;

public class UpdateTaskDto
{
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Title { get; set; } = null!;

    [Required]
    [EnumDataType(typeof(TaskItemStatus))]
    public TaskItemStatus Status { get; set; }

    public DateTime? DueDate { get; set; }
}