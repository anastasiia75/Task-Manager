using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDomain.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public bool IsDone { get; set; }
        public DateTime Deadline { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}
