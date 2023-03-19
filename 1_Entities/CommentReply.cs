using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace TaskManagement.Entities;

public class CommentReply
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime RepliedAt { get; set; }
    public Guid RepliedBy { get; set; }
    public Guid CommentId { get; set; }

    public virtual Employee Replier { get; set; }
    public virtual TaskComment Comment { get; set; }
}

public class CommentReplyConfig : IEntityTypeConfiguration<CommentReply>
{
    public void Configure(EntityTypeBuilder<CommentReply> entity)
    {
        entity.ToTable("comments_replies");
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

        entity.Property(e => e.RepliedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("replied_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.RepliedBy)
            .IsRequired()
            .HasColumnName("replied_by")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.CommentId)
            .IsRequired()
            .HasColumnName("comment_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.RepliedBy, "comments_replies_replied_by_fk_index");
        entity.HasOne(reply => reply.Replier)
            .WithMany(employee => employee.Replies)
            .HasForeignKey(reply => reply.RepliedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_comments_replies_fk");

        entity.HasIndex(e => e.CommentId, "comments_replies_comment_id_fk_index");
        entity.HasOne(reply => reply.Comment)
            .WithMany(comment => comment.Replies)
            .HasForeignKey(reply => reply.CommentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("tasks_comments_comments_replies_fk");

    }
}

public class CommentReplyFaker : Faker<CommentReply> {
    public CommentReplyFaker()
    {
        RuleFor(o => o.Text, f => f.Lorem.Sentences(f.Random.Byte(3, 6)));
        RuleFor(o => o.RepliedAt, f => f.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddMonths(3)));
    }
}
