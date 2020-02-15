using AutoMapper;
using TMS.Application.ViewModels;
using TMS.Domain.Tasks.Commands;

namespace TMS.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<CreateTaskViewModel, CreateTaskCommand>()
                .ConvertUsing<CreateTaskViewModelToCreateTaskCommand>();

            CreateMap<UpdateTaskViewModel, UpdateTaskCommand>()
                .ConstructUsing(c => new UpdateTaskCommand(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.StartDate,
                    c.FinishDate,
                    c.State));

            CreateMap<TaskViewModel, ExcludeTaskCommand>()
                .ConstructUsing(c => new ExcludeTaskCommand(c.Id));

            CreateMap<AddSubtaskViewModel, AddSubtaskCommand>()
                .ConstructUsing(c => new AddSubtaskCommand(
                    c.Name,
                    c.Description,
                    c.TaskId));

            CreateMap<SubtaskViewModel, UpdateSubtaskCommand>()
                .ConstructUsing(c => new UpdateSubtaskCommand(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.State,
                    c.TaskId));

            CreateMap<SubtaskViewModel, ExcludeSubtaskCommand>()
                .ConstructUsing(c => new ExcludeSubtaskCommand(c.Id));

        }
    }
}
