using AutoMapper;
using MediatR;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Projects;

public class GetUserProjectsHandler : IRequestHandler<GetUserProjectsQuery, List<ProjectResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserProjectsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<ProjectResponseDto>> Handle(GetUserProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var repo = _unitOfWork.Repository<Project>();
        var projects = await repo.FindAsync(p => p.UserId == request.UserId);

        return _mapper.Map<List<ProjectResponseDto>>(projects);
    }
}