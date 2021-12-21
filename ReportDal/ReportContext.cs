using Microsoft.EntityFrameworkCore;

namespace ReportDal
{
    public class ReportContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ReportDal.Report> Reports { get; set; }
        
        
        public string DbPath { get; private set; }

        public ReportContext(DbContextOptions<ReportContext> options) : base(options)
        {
             Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(employee => employee.Boss)
                .WithMany()
                .HasForeignKey(employee => employee.BossId).IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Task>()
                .HasOne(task => task.Employee)
                .WithMany()
                .HasForeignKey(task => task.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<TaskModification>()
                .HasOne(taskModification => taskModification.Task)
                .WithMany()
                .HasForeignKey(taskModification => taskModification.TaskId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}