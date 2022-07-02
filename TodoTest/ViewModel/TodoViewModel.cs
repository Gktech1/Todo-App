using System;
using TodoTest.Models;

namespace TodoTest.ViewModel
{
    public class TodoViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }    
        public DateTime Schedule { get; set; }
        public SelectedTodos TodoList { get; set; }
    }
}
