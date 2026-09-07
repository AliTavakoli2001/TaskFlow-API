using MediatR;
using TaskFlow.Application.DTOs;

namespace TaskFlow.Application.Features.Task;

public record CreateTaskCommand(Guid ProjectId, CreateTaskDto Dto) : IRequest<TaskResponseDto>;