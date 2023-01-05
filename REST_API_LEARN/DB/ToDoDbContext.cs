using Microsoft.EntityFrameworkCore;
using REST_API_LEARN.Models;
using System.Collections.Generic;

namespace REST_API_LEARN.DB
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                    new TodoItem { Id = 1, Name = "Write to Tom", IsComplete = false },
                    new TodoItem { Id = 2, Name = "Kill Nessy", IsComplete = true },
                    new TodoItem { Id = 3, Name = "Buy milk", IsComplete = false }
            );
        }
        public DbSet<TodoItem> ToDoRows { get; set; } = null!;


    }
}
