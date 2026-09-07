using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Task;

public record GetTasksByProjectQuery(Guid ProjectId) : IRequest<List<TaskResponseDto>>;