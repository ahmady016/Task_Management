using System.Reflection;
using Microsoft.EntityFrameworkCore;

using TaskManagement.Entities;

namespace TaskManagement.DB;

public partial class TaskManagementContext : DbContext
{
    public TaskManagementContext() {}
    public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options) {}

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamsMembers { get; set; }

    public DbSet<Project> Projects { get; set; }
    public DbSet<Task> Tasks { get; set; }

    public DbSet<TaskAssignment> Assignments { get; set; }
    public DbSet<TaskAction> Actions { get; set; }
    public DbSet<TaskAttachment> Attachments { get; set; }
    public DbSet<TaskComment> Comments { get; set; }
    public DbSet<TaskState> States { get; set; }

    public DbSet<AppLabel> Labels { get; set; }
    public DbSet<TaskLabel> TasksLabels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
