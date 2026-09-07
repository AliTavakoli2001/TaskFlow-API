using MediatR;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Task;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTaskHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<TaskItem>();
        var existing = await repo.GetByIdAsync(request.TaskId);

        if (existing != null)
        {
            repo.Delete(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        return false;
    }
}