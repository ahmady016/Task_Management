using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Entities;

public class TaskLabel
{
    public Guid TaskId { get; set; }
    public Guid LabelId { get; set; }

    public virtual AppTask Task { get; set; }
    public virtual AppLabel Label { get; set; }
}

public class TaskLabelConfig : IEntityTypeConfiguration<TaskLabel>
{
    public void Configure(EntityTypeBuilder<TaskLabel> entity)
    {
        entity.ToTable("tasks_labels");
        entity.HasKey(e => new { e.TaskId, e.LabelId });

        entity.Property(e => e.TaskId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.LabelId)
            .IsRequired()
            .HasColumnName("label_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.TaskId, "tasks_labels_task_id_fk_index");
        entity.HasOne(taskLabel => taskLabel.Task)
            .WithMany(task => task.Labels)
            .HasForeignKey(taskLabel => taskLabel.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_labels_fk");

        entity.HasIndex(e => e.LabelId, "tasks_labels_label_id_fk_index");
        entity.HasOne(taskLabel => taskLabel.Label)
            .WithMany(label => label.Tasks)
            .HasForeignKey(taskLabel => taskLabel.LabelId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("labels_tasks_labels_fk");

    }
}
