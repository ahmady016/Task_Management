using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Entities;

public class TeamMember
{
    public Guid TeamId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public virtual Team Team { get; set; }
    public virtual Employee Employee { get; set; }
}

public class TeamMemberConfig : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> entity)
    {
        entity.ToTable("teams_members");
        entity.HasKey(e => new { e.TeamId, e.EmployeeId });

        entity.Property(e => e.TeamId)
            .IsRequired()
            .HasColumnName("team_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.EmployeeId)
            .IsRequired()
            .HasColumnName("employee_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.JoinedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSDATETIME()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2(3)");

        entity.HasIndex(e => e.TeamId, "teams_members_team_id_fk_index");
        entity.HasOne(member => member.Team)
            .WithMany(team => team.Members)
            .HasForeignKey(member => member.TeamId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("teams_teams_members_fk");

        entity.HasIndex(e => e.EmployeeId, "teams_members_employee_id_fk_index");
        entity.HasOne(member => member.Employee)
            .WithMany(employee => employee.JoinedTeams)
            .HasForeignKey(member => member.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("employees_teams_members_fk");

    }
}
