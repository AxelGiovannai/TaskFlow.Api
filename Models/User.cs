using TaskFlow.Api.Models.Enums;

namespace TaskFlow.Api.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public UserRole Role { get; set; } = UserRole.User;

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}