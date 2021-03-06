﻿using System;
using System.ComponentModel.DataAnnotations;
using TMS.Domain;

namespace TMS.Application.ViewModels
{
    public class SubtaskViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        public States State { get; set; }

        public Guid TaskId { get; set; }
    }
}
