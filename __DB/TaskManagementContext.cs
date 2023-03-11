using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.DB;

public partial class TaskManagementContext : DbContext
{
    public TaskManagementContext() {}
    public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
