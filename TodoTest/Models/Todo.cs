using System.ComponentModel.DataAnnotations;
using System;
using TodoTest.Models;

namespace TodoTest.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
       

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        
        public DateTime Schedule { get; set; }
    }
}
