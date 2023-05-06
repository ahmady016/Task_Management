using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Views;

public partial class AssigneesTasksStatsView
{
    public Guid? AssignedEmployeeId { get; set; }
    public string AssignedEmployeeName { get; set; }
    public Guid? AssignedTeamId { get; set; }
    public string AssignedTeamName { get; set; }
    public int Completed { get; set; }
    public int Overdue { get; set; }
    public int Running { get; set; }
    public int Total { get; set; }
    public int Important { get; set; }
}

public class AssigneesTasksStatsViewConfig : IEntityTypeConfiguration<AssigneesTasksStatsView>
{
    public void Configure(EntityTypeBuilder<AssigneesTasksStatsView> entity)
    {
        entity.ToView("assignees_tasks_stats_view");
        entity.HasNoKey();

        entity.Property(e => e.AssignedEmployeeId).HasColumnName("assigned_employee_id");
        entity.Property(e => e.AssignedEmployeeName).HasColumnName("assigned_employee_name");
        entity.Property(e => e.AssignedTeamId).HasColumnName("assigned_team_id");
        entity.Property(e => e.AssignedTeamName).HasColumnName("assigned_team_name");
        entity.Property(e => e.Completed).HasColumnName("completed");
        entity.Property(e => e.Overdue).HasColumnName("overdue");
        entity.Property(e => e.Running).HasColumnName("running");
        entity.Property(e => e.Total).HasColumnName("total");
        entity.Property(e => e.Important).HasColumnName("important");
    }
}
