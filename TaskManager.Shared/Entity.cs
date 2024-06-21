using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Shared
{
    public class Entity
    {
        [Required]
        [Key]
        [Column(name:"id")]
        public string? Id { get; set; }
    }
}