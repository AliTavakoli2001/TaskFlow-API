using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Projects;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ProjectResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProjectHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProjectResponseDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Dto.Name,
            Description = request.Dto.Description,
        };
        await _unitOfWork.Repository<Project>().AddAsync(project);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<ProjectResponseDto>(project);
    }
}