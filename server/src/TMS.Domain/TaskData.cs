using FluentValidation;
using System;
using System.Collections.Generic;
using TMS.Domain.Constants;
using TMS.Domain.Core.Models;

namespace TMS.Domain
{
    public class TaskData : Entity<TaskData>
    {
        public TaskData(Guid id, string name, string description, DateTime? startDate, DateTime? finishDate, States state)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDate = startDate;
            FinishDate = finishDate;
            State = (int)state;

            Subtasks = new List<Subtask>();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? FinishDate { get; private set; }
        public int State { get; private set; }


        // EF Property Navegation
        public virtual ICollection<Subtask> Subtasks { get; set; }

        // EF Constructor
        protected TaskData() { }


        public override bool IsValid()
        {
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage(ValidationMessagesTaskData.Error_Name_NoEmpity)
                .Length(2, 200).WithMessage(ValidationMessagesTaskData.Error_Name_Length);

            RuleFor(r => r.StartDate)
                .LessThan(r => r.FinishDate).When(x => x.FinishDate != null)
                .WithMessage(ValidationMessagesTaskData.Error_StartDate_LessThan);

            RuleFor(r => r.FinishDate)
                .Null().When(r => r.StartDate == null).WithMessage(ValidationMessagesTaskData.Error_FinishDate_Null)
                .GreaterThan(r => r.StartDate).WithMessage(ValidationMessagesTaskData.Error_FinishDate_GreaterThan);

            ValidationResult = Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
