using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ToDoContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<User> Users { get; set; }
    }
}