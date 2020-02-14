using System;
using System.Collections.Generic;
using System.Text;
using TMS.Domain;

namespace TMS.Application.ViewModels
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public States State { get; set; }
        public ICollection<SubtaskViewModel> Subtasks { get; set; }
    }
}
