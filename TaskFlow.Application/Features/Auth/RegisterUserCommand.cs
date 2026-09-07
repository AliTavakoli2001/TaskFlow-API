using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Auth;

public record RegisterUserCommand(RegisterUserDto Dto) : IRequest<AuthResponseDto>;