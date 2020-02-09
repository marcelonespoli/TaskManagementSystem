using FluentValidation;
using System;
using TMS.Domain.Constants;
using TMS.Domain.Core.Models;

namespace TMS.Domain
{
    public class Subtask : Entity<Subtask>
    {
        public Subtask(Guid id, string name, string description, States state, Guid taskId)
        {
            Id = id;
            Name = name;
            Description = description;
            State = (int)state;
            TaskId = taskId;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public int State { get; private set; }
        public Guid TaskId { get; set; }


        // EF Property Navegation
        public virtual TaskData Task { get; set; }

        // EF Constructor
        protected Subtask() { }


        public override bool IsValid()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(ValidationMessagesSubtask.Error_Name_NotEmpty)
                .Length(2, 250).WithMessage(ValidationMessagesSubtask.Error_Name_Length);

            RuleFor(r => r.TaskId)
                .NotNull().WithMessage(ValidationMessagesSubtask.Error_TaskId_NullOrEmpty)
                .NotEmpty().WithMessage(ValidationMessagesSubtask.Error_TaskId_NullOrEmpty);

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
