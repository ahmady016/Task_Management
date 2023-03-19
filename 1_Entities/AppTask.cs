using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

using TaskManagement.Common;

namespace TaskManagement.Entities;

public class AppTask
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskPriorities PriorityId { get; set; } = TaskPriorities.Normal;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Guid ProjectId { get; set; }
    public Guid CreatedBy { get; set; }

    public virtual Project Project { get; set; }
    public virtual Employee Creator { get; set; }
    public virtual ICollection<TaskStatus> Statuses { get; set; } = new HashSet<TaskStatus>();
    public virtual ICollection<TaskAssignment> Assignees { get; set; } = new HashSet<TaskAssignment>();
    public virtual ICollection<TaskAction> Actions { get; set; } = new HashSet<TaskAction>();
}

public class AppTaskConfig : IEntityTypeConfiguration<AppTask>
{
    public void Configure(EntityTypeBuilder<AppTask> entity)
    {
        entity.ToTable("tasks");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("title")
            .HasColumnType("nvarchar(100)");

        entity.HasIndex(e => e.Title)
            .HasDatabaseName("tasks_title_unique_index")
            .IsUnique();

        entity.Property(e => e.Description)
            .HasMaxLength(1000)
            .HasColumnName("description")
            .HasColumnType("nvarchar(1000)");

        entity.Property(e => e.PriorityId)
            .IsRequired()
            .HasDefaultValue(TaskPriorities.Normal)
            .HasColumnName("priority_id")
            .HasColumnType("tinyint");

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.DueDate)
            .HasColumnName("due_date")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.CompletedAt)
            .HasColumnName("completed_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.ProjectId)
            .IsRequired()
            .HasColumnName("project_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.ProjectId, "employees_project_id_fk_index");
        entity.HasOne(task => task.Project)
            .WithMany(project => project.Tasks)
            .HasForeignKey(task => task.ProjectId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("projects_tasks_fk");

        entity.HasIndex(e => e.CreatedBy, "employees_tasks_created_by_fk_index");
        entity.HasOne(task => task.Creator)
            .WithMany(employee => employee.CreatedTasks)
            .HasForeignKey(task => task.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_created_tasks_fk");

    }
}

public class AppTaskFaker : Faker<AppTask> {
    private short counter = 1;
    public AppTaskFaker()
    {
        RuleFor(o => o.Title, f => $"{counter++}_{f.Commerce.ProductName}");
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.PriorityId, f => f.PickRandom<TaskPriorities>());
        RuleFor(o => o.DueDate, f => f.Date.Between(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow.AddMonths(3)).OrNull(f, 0.4f));
        RuleFor(o => o.CompletedAt, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(6)).OrNull(f, 0.6f));
    }
}
