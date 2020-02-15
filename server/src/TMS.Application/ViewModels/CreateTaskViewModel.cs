using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMS.Application.ViewModels
{
    public class CreateTaskViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public ICollection<SubtaskViewModel> Subtasks { get; set; }

        public CreateTaskViewModel()
        {
            Subtasks = new List<SubtaskViewModel>();
        }
    }
}
