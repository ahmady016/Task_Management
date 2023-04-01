using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

using TaskManagement.Common;

namespace TaskManagement.Entities;

public class TaskAssignment
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid AssignedBy { get; set; }
    public Guid AssignedTo { get; set; }
    public AssigneeTypes AssigneeType { get; set; }
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LeftAt { get; set; }
    public string LeaveReason { get; set; }

    public virtual AppTask Task { get; set; }
    public virtual Employee Assigner { get; set; }
    public virtual Employee AssignedEmployee { get; set; }
    public virtual Team AssignedTeam { get; set; }
}

public class TaskAssignmentConfig : IEntityTypeConfiguration<TaskAssignment>
{
    public void Configure(EntityTypeBuilder<TaskAssignment> entity)
    {
        entity.ToTable("tasks_assignments");
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
            .HasDatabaseName("task_assignment_unique_index")
            .IsUnique();

        entity.Property(e => e.AssigneeType)
            .IsRequired()
            .HasMaxLength(30)
            .HasColumnName("assignee_type")
            .HasColumnType("varchar(30)")
            .HasConversion(value => value.ToString(), value => Enum.Parse<AssigneeTypes>(value));

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

        entity.HasIndex(e => e.TaskId, "tasks_assignments_task_id_fk_index");
        entity.HasOne(taskAssignee => taskAssignee.Task)
            .WithMany(task => task.Assignees)
            .HasForeignKey(taskAssignee => taskAssignee.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_assignments_fk");

        entity.HasIndex(e => e.AssignedBy, "tasks_assignments_assigned_by_fk_index");
        entity.HasOne(taskAssignee => taskAssignee.Assigner)
            .WithMany(employee => employee.AssignerTasks)
            .HasForeignKey(taskAssignee => taskAssignee.AssignedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("assigners_tasks_assignments_fk");

        entity.HasIndex(e => e.AssignedTo, "tasks_assignments_assigned_to_fk_index");
        entity.HasOne(taskAssignee => taskAssignee.AssignedEmployee)
            .WithMany(employee => employee.AssignedTasks)
            .HasForeignKey(taskAssignee => taskAssignee.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_tasks_assignments_fk");
        entity.HasOne(taskAssignee => taskAssignee.AssignedTeam)
            .WithMany(team => team.AssignedTasks)
            .HasForeignKey(taskAssignee => taskAssignee.AssignedTo)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("teams_tasks_assignments_fk");

    }
}

public class TaskAssignmentFaker : Faker<TaskAssignment> {
    public TaskAssignmentFaker()
    {
        RuleFor(o => o.AssignedAt, f => f.Date.Between(DateTime.UtcNow.AddMonths(-6), DateTime.UtcNow));
        RuleFor(o => o.LeftAt, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(4)).OrNull(f, 0.8f));
        RuleFor(o => o.LeaveReason, f => f.Lorem.Sentences(f.Random.Byte(1, 5).OrNull(f, 0.8f)));
    }
}
