using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Views;

public partial class ProjectsWithTasksView
{
    public Guid ProjectId { get; set; }
    public string ProjectTitle { get; set; }
    public string ProjectDescription { get; set; }
    public DateTime ProjectCreatedAt { get; set; }
    public DateTime? ProjectCompletedAt { get; set; }
    public Guid ProjectCreatorId { get; set; }
    public string ProjectCreatorName { get; set; }
    public Guid ProjectManagerId { get; set; }
    public string ProjectManagerName { get; set; }
    public Guid TaskId { get; set; }
    public string TaskTitle { get; set; }
    public string TaskDescription { get; set; }
    public string TaskPriority { get; set; }
    public DateTime TaskCreatedAt { get; set; }
    public DateTime? TaskCompletedAt { get; set; }
    public DateTime? TaskDueDate { get; set; }
    public Guid TaskCreatorId { get; set; }
    public string TaskCreatorName { get; set; }
}

public class ProjectsWithTasksViewConfig : IEntityTypeConfiguration<ProjectsWithTasksView>
{
    public void Configure(EntityTypeBuilder<ProjectsWithTasksView> entity)
    {
        entity.ToView("projects_with_tasks_view");
        entity.HasNoKey();

        entity.Property(e => e.ProjectId).HasColumnName("project_id");
        entity.Property(e => e.ProjectTitle).HasColumnName("project_title");
        entity.Property(e => e.ProjectDescription).HasColumnName("project_description");
        entity.Property(e => e.ProjectCreatedAt).HasColumnName("project_created_at");
        entity.Property(e => e.ProjectCompletedAt).HasColumnName("project_completed_at");
        entity.Property(e => e.ProjectCreatorId).HasColumnName("project_creator_id");
        entity.Property(e => e.ProjectCreatorName).HasColumnName("project_creator_name");
        entity.Property(e => e.ProjectManagerId).HasColumnName("project_manager_id");
        entity.Property(e => e.ProjectManagerName).HasColumnName("project_manager_name");

        entity.Property(e => e.TaskId).HasColumnName("task_id");
        entity.Property(e => e.TaskTitle).HasColumnName("task_title");
        entity.Property(e => e.TaskDescription).HasColumnName("task_description");
        entity.Property(e => e.TaskPriority).HasColumnName("task_priority");
        entity.Property(e => e.TaskCreatedAt).HasColumnName("task_created_at");
        entity.Property(e => e.TaskCompletedAt).HasColumnName("task_completed_at");
        entity.Property(e => e.TaskDueDate).HasColumnName("task_due_date");
        entity.Property(e => e.TaskCreatorId).HasColumnName("task_creator_id");
        entity.Property(e => e.TaskCreatorName).HasColumnName("task_creator_name");
    }
}
