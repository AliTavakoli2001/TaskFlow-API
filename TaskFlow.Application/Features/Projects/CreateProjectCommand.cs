using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Projects;

public record CreateProjectCommand(Guid UserId, CreateProjectDto Dto) : IRequest<ProjectResponseDto>;