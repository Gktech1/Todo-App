using System.ComponentModel.DataAnnotations;
using System;

namespace TodoWeek7.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
       

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Schedule {get; set;}
    }
}
