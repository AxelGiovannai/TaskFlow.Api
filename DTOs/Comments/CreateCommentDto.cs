using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.DTOs.Comments;

public class CreateCommentDto
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; } = null!;
}