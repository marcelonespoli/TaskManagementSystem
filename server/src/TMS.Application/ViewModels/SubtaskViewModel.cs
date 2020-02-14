using System;
using System.Collections.Generic;
using System.Text;
using TMS.Domain;

namespace TMS.Application.ViewModels
{
    public class SubtaskViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public States State { get; set; }
        public Guid TaskId { get; set; }
    }
}
