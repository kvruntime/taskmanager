using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Shared
{
    public class TaskItem : Entity
    {
        [Required]
        [Column(name: "taskname")]
        public string TaskName { get; set; }
        [Column(name: "iscomplete")]
        public bool IsComplete { get; set; } = false;

        public static TaskItem CreateFromDto(TaskItemCreateDto dto)
        {
            return new TaskItem()
            {
                Id = Guid.NewGuid().ToString(),
                TaskName = dto.TaskName,
                IsComplete = false
            };
        }

        public TaskItemReadDto ReadToDto()
        {
            return new TaskItemReadDto()
            {
                Id = this.Id,
                TaskName = this.TaskName,
                IsComplete = this.IsComplete,
            };
        }
        public void UpdateFromDto(TaskItemReadDto dto)
        {
            this.TaskName = dto.TaskName;
            this.IsComplete = dto.IsComplete;
        }
    }
}