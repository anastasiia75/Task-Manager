using Microsoft.EntityFrameworkCore;
using TaskManagerDomain.Models;
using Assignment = TaskManagerDomain.Models.Assignment;

namespace TaskManagerDAL
{
    public class DataContext : DbContext

    {
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
        }
    }
}