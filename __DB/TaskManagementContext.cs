using System.Reflection;
using Microsoft.EntityFrameworkCore;

using TaskManagement.Entities;
using TaskManagement.Views;

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
    public DbSet<AppTask> Tasks { get; set; }

    public DbSet<TaskState> States { get; set; }
    public DbSet<TaskAssignment> Assignments { get; set; }
    public DbSet<TaskAction> Actions { get; set; }
    public DbSet<TaskAttachment> Attachments { get; set; }
    public DbSet<TaskComment> Comments { get; set; }

    public DbSet<AppLabel> Labels { get; set; }
    public DbSet<TaskLabel> TasksLabels { get; set; }

    public DbSet<HierarchiesWithEmployeesView> HierarchiesWithEmployeesView { get; set; }
    public DbSet<HierarchiesTreeView> HierarchiesTreeView { get; set; }

    public DbSet<LabelsWithTasksView> LabelsWithTasksView { get; set; }
    public DbSet<ProjectsWithTasksView> ProjectsWithTasksView { get; set; }

    public DbSet<TasksWithAllAssigneesView> TasksWithAllAssigneesView { get; set; }
    public DbSet<TasksWithLastAssigneeView> TasksWithLastAssigneeView { get; set; }

    public DbSet<ProjectsTasksStatsView> ProjectsTasksStatsView { get; set; }
    public DbSet<AssigneesTasksStatsView> AssigneesTasksStatsView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
