using TaskFlow.Api.Models;

namespace TaskFlow.Api.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}