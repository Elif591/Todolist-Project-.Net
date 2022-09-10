using Microsoft.EntityFrameworkCore;
using TodoListProject.Backend.Models.Entities;

namespace TodoListProject.Backend.Models.DataContext
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-JA8MVMV; Database=TodoDB; uid=sa; pwd=1234;");
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Tasks> Tasks { get; set; }


    }

}

