using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TMS.Application.ViewModels;
using TMS.Domain.Tasks.Commands;

namespace TMS.Application.AutoMapper
{
    public class CreateTaskViewModelToCreateTaskCommand : ITypeConverter<CreateTaskViewModel, CreateTaskCommand>
    {
        public CreateTaskCommand Convert(CreateTaskViewModel source, CreateTaskCommand destination, ResolutionContext context)
        {
            var id = Guid.NewGuid();
            var subtasks = new List<AddSubtaskCommand>();
            
            if (source.Subtasks != null && source.Subtasks.Any())
            {
                foreach (var item in source.Subtasks)
                {
                    var subtask = new AddSubtaskCommand(item.Name, item.Description, id);
                    subtasks.Add(subtask);
                }
            }

            return new CreateTaskCommand(id, source.Name, source.Description, source.StartDate, source.FinishDate, subtasks);
        }
    }
}
