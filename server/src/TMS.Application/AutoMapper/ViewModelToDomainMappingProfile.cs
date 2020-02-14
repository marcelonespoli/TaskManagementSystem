using AutoMapper;
using System.Linq;
using TMS.Application.ViewModels;
using TMS.Domain.Tasks.Commands;

namespace TMS.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<TaskViewModel, CreateTaskCommand>()
                .ConstructUsing(c => new CreateTaskCommand(
                    c.Name,
                    c.Description,
                    c.StartDate,
                    c.FinishDate,
                    c.Subtasks.Select(s => new AddSubtaskCommand(s.Name, s.Description, s.TaskId)).ToList()
                ));

            CreateMap<TaskViewModel, UpdateTaskCommand>()
                .ConstructUsing(c => new UpdateTaskCommand(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.StartDate,
                    c.FinishDate,
                    c.State));

            CreateMap<TaskViewModel, ExcludeTaskCommand>()
                .ConstructUsing(c => new ExcludeTaskCommand(c.Id));

            CreateMap<SubtaskViewModel, AddSubtaskCommand>()
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
