using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

using TaskManagement.Common;

namespace TaskManagement.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ProjectTypes Type { get; set; } = ProjectTypes.WaterFall;
    public ProjectStatuses Status { get; set; } = ProjectStatuses.Waiting;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ManageBy { get; set; }

    public virtual Employee Creator { get; set; }
    public virtual Employee Manager { get; set; }
    public virtual ICollection<AppTask> Tasks { get; set; } = new HashSet<AppTask>();
}

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> entity)
    {
        entity.ToTable("projects");
        entity.HasKey(e => e.Id);

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
            .HasDatabaseName("projects_title_unique_index")
            .IsUnique();

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("description")
            .HasColumnType("nvarchar(1000)");

        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("type")
            .HasColumnType("varchar(10)")
            .HasDefaultValue(ProjectTypes.WaterFall)
            .HasConversion(value => value.ToString(), value => Enum.Parse<ProjectTypes>(value));

        entity.Property(e => e.Status)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("status")
            .HasColumnType("varchar(10)")
            .HasDefaultValue(ProjectStatuses.Waiting)
            .HasConversion(value => value.ToString(), value => Enum.Parse<ProjectStatuses>(value));

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.CompletedAt)
            .HasColumnName("completed_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.ManageBy)
            .HasColumnName("manage_by")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.CreatedBy, "employees_projects_created_by_fk_index");
        entity.HasOne(project => project.Creator)
            .WithMany(employee => employee.CreatedProject)
            .HasForeignKey(project => project.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_created_projects_fk");

        entity.HasIndex(e => e.ManageBy, "employees_projects_manage_by_fk_index");
        entity.HasOne(project => project.Manager)
            .WithMany(employee => employee.ManagedProject)
            .HasForeignKey(project => project.ManageBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_managed_projects_fk");

    }
}

public class ProjectFaker : Faker<Project> {
    private short counter = 1;
    public ProjectFaker()
    {
        RuleFor(o => o.Title, f => $"{counter++}_{f.Commerce.ProductName()}");
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.Type, f => f.PickRandom<ProjectTypes>());
        RuleFor(o => o.Status, f => f.PickRandom<ProjectStatuses>());
        RuleFor(o => o.CompletedAt, f => f.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow).OrNull(f, 0.8f));
    }
}
