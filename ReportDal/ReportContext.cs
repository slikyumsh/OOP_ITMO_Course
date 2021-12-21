using Microsoft.EntityFrameworkCore;

namespace ReportDal
{
    public class ReportContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ReportDal.Report> Reports { get; set; }
        
        
        public string DbPath { get; private set; }

        public ReportContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}