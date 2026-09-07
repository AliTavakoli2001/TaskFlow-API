namespace TaskFlow.Application.DTOs;

public class RegisterUserDto
{
    public string FullName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}

public class LoginUserDto
{
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string FullName { get; set; } = String.Empty;
}