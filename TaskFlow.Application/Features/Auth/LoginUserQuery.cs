using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Auth;

public record LoginUserQuery(LoginUserDto Dto) : IRequest<AuthResponseDto>;