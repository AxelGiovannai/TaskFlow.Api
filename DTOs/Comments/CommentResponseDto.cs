namespace TaskFlow.Api.DTOs.Comments;

public class CommentResponseDto
{
    public int Id { get; set; }

    public string Content { get; set; } = null!;

    public int TaskItemId { get; set; }
}