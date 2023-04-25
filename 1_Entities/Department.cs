using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace TaskManagement.Entities;

public class Department
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? ManagerId { get; set; }
    public Guid? DepartmentId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
}

public class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> entity)
    {
        entity.ToTable("departments");
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
            .HasDatabaseName("departments_title_unique_index")
            .IsUnique();

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("description")
            .HasColumnType("nvarchar(1000)");

        entity.Property(e => e.ManagerId)
            .HasColumnName("manager_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.DepartmentId)
            .HasColumnName("department_id")
            .HasColumnType("uniqueidentifier");
    }
}

public class DepartmentFaker : Faker<Department> {
    private short counter = 1;
    public DepartmentFaker()
    {
        RuleFor(o => o.Title, f => $"{counter++}_{f.Commerce.Department()}");
        RuleFor(o => o.Description, f => f.Commerce.ProductAdjective());
    }
}
