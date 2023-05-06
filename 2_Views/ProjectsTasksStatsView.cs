using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Views;

public partial class ProjectsTasksStatsView
{
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; }
    public int Completed { get; set; }
    public int Overdue { get; set; }
    public int Running { get; set; }
    public int Total { get; set; }
    public int Important { get; set; }
}

public class ProjectsTasksStatsViewConfig : IEntityTypeConfiguration<ProjectsTasksStatsView>
{
    public void Configure(EntityTypeBuilder<ProjectsTasksStatsView> entity)
    {
        entity.ToView("projects_tasks_stats_view");
        entity.HasNoKey();

        entity.Property(e => e.ProjectId).HasColumnName("project_id");
        entity.Property(e => e.ProjectTitle).HasColumnName("project_title");
        entity.Property(e => e.Completed).HasColumnName("completed");
        entity.Property(e => e.Overdue).HasColumnName("overdue");
        entity.Property(e => e.Running).HasColumnName("running");
        entity.Property(e => e.Total).HasColumnName("total");
        entity.Property(e => e.Important).HasColumnName("important");
    }
}
