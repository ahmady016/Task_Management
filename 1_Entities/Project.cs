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
    public ProjectTypes TypeId { get; set; } = ProjectTypes.WaterFall;
    public Guid? ManagerId { get; set; }

    public virtual Employee Manager { get; set; }
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

        entity.Property(e => e.TypeId)
            .IsRequired()
            .HasDefaultValue(ProjectTypes.WaterFall)
            .HasColumnName("type_id")
            .HasColumnType("tinyint");

        entity.Property(e => e.ManagerId)
            .HasColumnName("manager_id")
            .HasColumnType("uniqueidentifier");
    }
}

public class ProjectFaker : Faker<Project> {
    private short counter = 1;
    public ProjectFaker()
    {
        RuleFor(o => o.Title, f => $"{counter++}_{f.Commerce.ProductName}");
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.TypeId, f => f.PickRandom<ProjectTypes>());
    }
}
