using AutoMapper;
using TMS.Domain;
using TMS.Application.ViewModels;

namespace TMS.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<TaskData,TaskViewModel>();
            CreateMap<Subtask, SubtaskViewModel>();
        }
    }
}
