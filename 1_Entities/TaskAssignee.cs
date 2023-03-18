using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

using TaskManagement.Common;

namespace TaskManagement.Entities;

public class TaskAssignee
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid AssignedBy { get; set; }
    public Guid AssignedTo { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LeftAt { get; set; }
    public string LeaveReason { get; set; }

    public virtual AppTask Task { get; set; }
    public virtual Employee Assigner { get; set; }
    public virtual Employee Assignee { get; set; }
}

public class TaskAssigneeConfig : IEntityTypeConfiguration<TaskAssignee>
{
    public void Configure(EntityTypeBuilder<TaskAssignee> entity)
    {
        entity.ToTable("tasks_assignees");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.TaskId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.AssignedBy)
            .IsRequired()
            .HasColumnName("assigned_by")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.AssignedTo)
            .IsRequired()
            .HasColumnName("assigned_to")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => new { e.TaskId, e.AssignedBy, e.AssignedTo })
            .IsUnique();

        entity.Property(e => e.AssignedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("assigned_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.LeftAt)
            .HasColumnName("left_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.LeaveReason)
            .HasMaxLength(500)
            .HasColumnName("leave_reason")
            .HasColumnType("nvarchar(500)");

        entity.HasIndex(e => e.TaskId, "tasks_assignees_task_id_fk_index");
        entity.HasOne(taskAssignee => taskAssignee.Task)
            .WithMany(task => task.Assignees)
            .HasForeignKey(taskAssignee => taskAssignee.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_assignees_fk");

        entity.HasIndex(e => e.AssignedBy, "tasks_assignees_assigned_by_fk_index");
        entity.HasOne(taskAssignee => taskAssignee.Assigner)
            .WithMany(employee => employee.AssignerTasks)
            .HasForeignKey(taskAssignee => taskAssignee.AssignedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("assigners_tasks_assignees_fk");

        entity.HasIndex(e => e.AssignedTo, "tasks_assignees_assigned_to_fk_index");
        entity.HasOne(taskAssignee => taskAssignee.Assignee)
            .WithMany(employee => employee.AssignedTasks)
            .HasForeignKey(taskAssignee => taskAssignee.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("assignees_tasks_assignees_fk");

    }
}

public class TaskAssigneeFaker : Faker<TaskAssignee> {
    public TaskAssigneeFaker()
    {
        RuleFor(o => o.AssignedAt, f => f.Date.Between(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow));
        RuleFor(o => o.LeftAt, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(4)).OrNull(f, 0.8f));
        RuleFor(o => o.LeaveReason, f => f.Lorem.Sentences(f.Random.Byte(1, 5).OrNull(f, 0.8f)));
    }
}
