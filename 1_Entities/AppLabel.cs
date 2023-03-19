using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace TaskManagement.Entities;

public class AppLabel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ColorName { get; set; }
    public string ColorCode { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; }

    public virtual Employee Creator { get; set; }
    public virtual ICollection<TaskLabel> Tasks { get; set; } = new HashSet<TaskLabel>();
}

public class AppLabelConfig : IEntityTypeConfiguration<AppLabel>
{
    public void Configure(EntityTypeBuilder<AppLabel> entity)
    {
        entity.ToTable("labels");
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
            .HasDatabaseName("labels_title_unique_index")
            .IsUnique();

        entity.Property(e => e.Description)
            .HasMaxLength(1000)
            .HasColumnName("description")
            .HasColumnType("nvarchar(1000)");

        entity.Property(e => e.ColorName)
            .HasMaxLength(50)
            .HasColumnName("color_name")
            .HasColumnType("varchar(50)");

        entity.Property(e => e.ColorCode)
            .HasMaxLength(50)
            .HasColumnName("color_code")
            .HasColumnType("varchar(50)");

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.CreatedBy, "labels_created_by_fk_index");
        entity.HasOne(label => label.Creator)
            .WithMany(employee => employee.CreatedLabels)
            .HasForeignKey(label => label.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_labels_fk");

    }
}

public class AppLabelFaker : Faker<AppLabel>
{
    private short counter = 1;
    public AppLabelFaker()
    {
        RuleFor(o => o.Title, f => $"{counter++}_{f.Commerce.ProductName}");
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.ColorName, f => f.Internet.Color());
        RuleFor(o => o.ColorCode, f => f.Internet.Color());
        RuleFor(o => o.CreatedAt, f => f.Date.Past());
    }
}
