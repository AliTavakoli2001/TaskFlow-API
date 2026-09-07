using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Task;

public record UpdateTaskCommand(Guid TaskId, UpdateTaskDto Dto) : IRequest<TaskResponseDto>;