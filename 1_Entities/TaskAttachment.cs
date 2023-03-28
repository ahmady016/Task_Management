using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace TaskManagement.Entities;

public class TaskAttachment
{
    public Guid Id { get; set; }
    public string FileUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid AttachedBy { get; set; }
    public Guid TaskId { get; set; }

    public Employee Attacher { get; set; }
    public AppTask Task { get; set; }
}

public class TaskAttachmentConfig : IEntityTypeConfiguration<TaskAttachment>
{
    public void Configure(EntityTypeBuilder<TaskAttachment> entity)
    {
        entity.ToTable("tasks_attachments");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.FileUrl)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("file_url")
            .HasColumnType("nvarchar(200)");

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.AttachedBy)
            .IsRequired()
            .HasColumnName("created_by")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.TaskId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.AttachedBy, "tasks_attachments_created_by_fk_index");
        entity.HasOne(attachment => attachment.Attacher)
            .WithMany(employee => employee.AttachedFiles)
            .HasForeignKey(attachment => attachment.AttachedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_tasks_attachments_fk");

        entity.HasIndex(e => e.TaskId, "tasks_attachments_task_id_fk_index");
        entity.HasOne(attachment => attachment.Task)
            .WithMany(task => task.Attachments)
            .HasForeignKey(attachment => attachment.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_attachments_fk");

    }
}

public class TaskAttachmentFaker : Faker<TaskAttachment>
{
    public TaskAttachmentFaker()
    {
        RuleFor(o => o.FileUrl, f => f.Image.PicsumUrl());
        RuleFor(o => o.CreatedAt, f => f.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddMonths(3)));
    }
}
