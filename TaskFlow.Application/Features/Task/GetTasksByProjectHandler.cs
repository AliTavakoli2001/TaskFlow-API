using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Task;

public class GetTasksByProjectHandler : IRequestHandler<GetTasksByProjectQuery, List<TaskResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTasksByProjectHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<TaskResponseDto>> Handle(GetTasksByProjectQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _unitOfWork.Repository<TaskItem>().FindAsync(t => t.Project.Id == request.ProjectId);
        return _mapper.Map<List<TaskResponseDto>>(tasks);

        /*var project = await _unitOfWork.Repository<Project>().GetByIdAsync(request.ProjectId);
        return _mapper.Map<List<TaskResponseDto>>(project.Tasks);*/
    }
}