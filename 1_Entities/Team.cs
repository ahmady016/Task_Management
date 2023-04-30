using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

namespace TaskManagement.Entities;

public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; }

    public virtual Employee Creator { get; set; }
    public virtual ICollection<TeamMember> Members { get; set; } = new HashSet<TeamMember>();
    public virtual ICollection<TaskAssignment> AssignedTasks { get; set; } = new HashSet<TaskAssignment>();
}

public class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> entity)
    {
        entity.ToTable("teams");
        entity.HasKey(t => t.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name")
            .HasColumnType("nvarchar(100)");

        entity.HasIndex(e => e.Name)
            .HasDatabaseName("teams_name_unique_index")
            .IsUnique();

        entity.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("description")
            .HasColumnType("nvarchar(1000)");

        entity.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2(3)");

        entity.Property(e => e.CreatedBy)
            .IsRequired()
            .HasColumnName("created_by")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.CreatedBy, "teams_created_by_fk_index");
        entity.HasOne(team => team.Creator)
            .WithMany(employee => employee.CreatedTeams)
            .HasForeignKey(team => team.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("employees_created_teams_fk");

    }
}

public class TeamFaker : Faker<Team>
{
    private short counter = 1;
    public TeamFaker()
    {
        RuleFor(o => o.Name, f => $"{counter++}_{f.Company.CompanyName()}");
        RuleFor(o => o.Description, f => f.Commerce.ProductDescription());
        RuleFor(o => o.CreatedAt, f => f.Date.Between(DateTime.UtcNow, DateTime.UtcNow.AddMonths(6)));
    }
}
