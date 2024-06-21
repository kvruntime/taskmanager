using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Shared
{
    public class TaskItemCreateDto
    {
         [Required]
         [MinLength(5)]
        public string TaskName { get; set; }
        // [DefaultValue(false)]
        // public bool IsComplete { get; set; } = false;
    }
}