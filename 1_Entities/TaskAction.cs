using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace TaskManagement.Entities;

public class TaskAction
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public Guid TakenBy { get; set; }
    public Guid TaskId { get; set; }

    public virtual Employee Taker { get; set; }
    public virtual AppTask Task { get; set; }
}

public class TaskActionConfig : IEntityTypeConfiguration<TaskAction>
{
    public void Configure(EntityTypeBuilder<TaskAction> entity)
    {
        entity.ToTable("tasks_actions");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName("description")
            .HasColumnType("nvarchar(500)");

        entity.Property(e => e.Date)
            .IsRequired()
            .HasColumnName("date")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.TakenBy)
            .IsRequired()
            .HasColumnName("taken_by")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.TaskId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.TakenBy, "tasks_actions_taken_by_fk_index");
        entity.HasOne(taskActions => taskActions.Taker)
            .WithMany(employee => employee.Actions)
            .HasForeignKey(taskActions => taskActions.TakenBy)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("employees_tasks_actions_fk");

        entity.HasIndex(e => e.TaskId, "tasks_actions_task_id_fk_index");
        entity.HasOne(taskActions => taskActions.Task)
            .WithMany(task => task.Actions)
            .HasForeignKey(taskActions => taskActions.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_actions_fk");

    }
}

public class TaskActionFaker : Faker<TaskAction> {
    public TaskActionFaker()
    {
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.Date, f => f.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddMonths(3)));
    }
}
