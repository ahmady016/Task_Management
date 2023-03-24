using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

using TaskManagement.Common;

namespace TaskManagement.Entities;

public class TaskState
{
    public Guid Id { get; set; }
    public TaskStates State { get; set; }
    public DateTime From { get; set; } = DateTime.UtcNow;
    public DateTime? To { get; set; }
    public Guid TaskId { get; set; }

    public virtual AppTask Task { get; set; }
}

public class TaskStateConfig : IEntityTypeConfiguration<TaskState>
{
    public void Configure(EntityTypeBuilder<TaskState> entity)
    {
        entity.ToTable("tasks_states");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.State)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("state")
            .HasColumnType("varchar(10)")
            .HasDefaultValue(TaskStates.Pending.ToString())
            .HasConversion(value => value.ToString(), value => Enum.Parse<TaskStates>(value));

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

        entity.HasIndex(e => e.TaskId, "tasks_states_task_id_fk_index");
        entity.HasOne(taskState => taskState.Task)
            .WithMany(task => task.States)
            .HasForeignKey(taskState => taskState.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_states_fk");

    }
}

public class TaskStateFaker : Faker<TaskState>
{
    public TaskStateFaker()
    {
        RuleFor(o => o.State, f => f.PickRandom<TaskStates>());
        RuleFor(o => o.To, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(6)).OrNull(f, 0.6f));
    }
}
