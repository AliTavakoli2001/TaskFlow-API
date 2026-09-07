using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Task;

public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateTaskHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TaskResponseDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            ProjectId = request.ProjectId,
            Title = request.Dto.Title,
            Description = request.Dto.Description,
            Priority = request.Dto.Priority,
            DueDate = request.Dto.DueDate,
        };

        await _unitOfWork.Repository<TaskItem>().AddAsync(task);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<TaskResponseDto>(task);
    }
}