using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.Application.ViewModels
{
    public class AddSubtaskViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public Guid TaskId { get; set; }
    }
}
