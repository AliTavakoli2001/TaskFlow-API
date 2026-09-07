using AutoMapper;
using TaskFlow.Application.DTOs;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // CreateMap<Project, ProjectResponseDto>()
        //     .ForMember(d => d.TaskCount, o => o.MapFrom(s => s.Tasks.Count));
        //
        // CreateMap<TaskItem, TaskResponseDto>()
        //     .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
        //     .ForMember(d => d.Priority, o => o.MapFrom(s => s.Priority));
    }
}