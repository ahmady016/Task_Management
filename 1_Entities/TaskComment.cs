using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace TaskManagement.Entities;

public class TaskComment
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CommentedAt { get; set; }
    public Guid CommentedBy { get; set; }
    public Guid TaskId { get; set; }

    public virtual Employee Commenter { get; set; }
    public virtual AppTask Task { get; set; }
    public virtual ICollection<CommentReply> Replies { get; set; } = new HashSet<CommentReply>();
}

public class TaskCommentConfig : IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> entity)
    {
        entity.ToTable("tasks_comments");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.Text)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("text")
            .HasColumnType("nvarchar(1000)");

        entity.Property(e => e.CommentedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("commented_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.CommentedBy)
            .IsRequired()
            .HasColumnName("commented_by")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.TaskId)
            .IsRequired()
            .HasColumnName("task_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.CommentedBy, "tasks_comments_commented_by_fk_index");
        entity.HasOne(comment => comment.Commenter)
            .WithMany(employee => employee.Comments)
            .HasForeignKey(comment => comment.CommentedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_tasks_comments_fk");

        entity.HasIndex(e => e.TaskId, "tasks_comments_task_id_fk_index");
        entity.HasOne(comment => comment.Task)
            .WithMany(task => task.Comments)
            .HasForeignKey(comment => comment.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_tasks_comments_fk");

    }
}

public class TaskCommentFaker : Faker<TaskComment> {
    public TaskCommentFaker()
    {
        RuleFor(o => o.Text, f => f.Lorem.Sentences(f.Random.Byte(3, 6)));
        RuleFor(o => o.CommentedAt, f => f.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddMonths(3)));
    }
}
