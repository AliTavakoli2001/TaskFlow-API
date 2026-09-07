using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Task;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, TaskResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskResponseDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<TaskItem>();

        var existing = await repo.GetByIdAsync(request.TaskId);
        if (existing == null) throw new KeyNotFoundException("Task not found!");

        existing.Title = request.Dto.Title;
        existing.Description = request.Dto.Description;
        existing.Priority = request.Dto.Priority;
        existing.Status = request.Dto.Status;
        existing.DueDate = request.Dto.DueDate;

        repo.Update(existing);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<TaskResponseDto>(existing);
    }
}