using TaskFlow.Api.DTOs.Auth;

namespace TaskFlow.Api.Services.Interfaces;

public interface IUserService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto);
    Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
}