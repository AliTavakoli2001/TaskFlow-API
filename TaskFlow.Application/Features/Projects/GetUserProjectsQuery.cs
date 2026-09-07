using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Projects;

public record GetUserProjectsQuery(Guid UserId) : IRequest<List<ProjectResponseDto>>;