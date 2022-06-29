using Microsoft.EntityFrameworkCore;
using TodoWeek7.Models;

namespace TodoTest.Infastructure
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) {}

        public DbSet<Todo> Todo { get; set; }
    }
}
  