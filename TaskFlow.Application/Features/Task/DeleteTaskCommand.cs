using MediatR;

namespace TaskFlow.Application.Features.Task;

public record DeleteTaskCommand(Guid TaskId) : IRequest<bool>;