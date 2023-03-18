using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

using TaskManagement.Common;

namespace TaskManagement.Entities;

public class TaskStatus
{
    public Guid Id { get; set; }
    public TaskStatuses StatusId { get; set; }
    public DateTime From { get; set; } = DateTime.UtcNow;
    public DateTime? To { get; set; }
    public Guid TaskId { get; set; }

    public virtual AppTask Task { get; set; }
}

public class TaskStatusConfig : IEntityTypeConfiguration<TaskStatus>
{
    public void Configure(EntityTypeBuilder<TaskStatus> entity)
    {
        entity.ToTable("tasks_statuses");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.StatusId)
            .IsRequired()
            .HasDefaultValue(TaskStatuses.Pending)
            .HasColumnName("status_id")
            .HasColumnType("tinyint");

        entity.Property(e => e.From)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("from")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.To)
            .HasColumnName("to")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.TaskId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.TaskId, "tasks_statuses_task_id_fk_index");
        entity.HasOne(taskStatus => taskStatus.Task)
            .WithMany(task => task.Statuses)
            .HasForeignKey(taskStatus => taskStatus.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_statuses_fk");

    }
}

public class TaskStatusFaker : Faker<TaskStatus> {
    public TaskStatusFaker()
    {
        RuleFor(o => o.StatusId, f => f.PickRandom<TaskStatuses>());
        RuleFor(o => o.To, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(6)).OrNull(f, 0.6f));
    }
}
