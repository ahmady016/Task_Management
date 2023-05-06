using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Views;

public partial class LabelsWithTasksView
{
    public Guid LabelId { get; set; }
    public string LabelName { get; set; }
    public Guid LabelCreatorId { get; set; }
    public string LabelCreatorName { get; set; }
    public Guid TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskPriority { get; set; }
    public DateTime TaskCreatedAt { get; set; }
    public DateTime? TaskDueDate { get; set; }
    public DateTime? TaskCompletedAt { get; set; }
    public Guid TaskCreatorId { get; set; }
    public string TaskCreatorName { get; set; }
    public Guid? TaskAssignedEmployeeId { get; set; }
    public string TaskAssignedEmployeeName { get; set; }
    public Guid? TaskAssignedTeamId { get; set; }
    public string TaskAssignedTeamName { get; set; }
}

public class LabelsWithTasksViewConfig : IEntityTypeConfiguration<LabelsWithTasksView>
{
    public void Configure(EntityTypeBuilder<LabelsWithTasksView> entity)
    {
        entity.ToView("labels_with_tasks_view");
        entity.HasNoKey();

        entity.Property(e => e.LabelId).HasColumnName("label_id");
        entity.Property(e => e.LabelName).HasColumnName("label_name");
        entity.Property(e => e.LabelCreatorId).HasColumnName("label_creator_id");
        entity.Property(e => e.LabelCreatorName).HasColumnName("label_creator_name");

        entity.Property(e => e.TaskId).HasColumnName("task_id");
        entity.Property(e => e.TaskName).HasColumnName("task_name");
        entity.Property(e => e.TaskPriority).HasColumnName("task_priority");
        entity.Property(e => e.TaskCreatedAt).HasColumnName("task_created_at");
        entity.Property(e => e.TaskDueDate).HasColumnName("task_due_date");
        entity.Property(e => e.TaskCompletedAt).HasColumnName("task_completed_at");
        entity.Property(e => e.TaskCreatorId).HasColumnName("task_creator_id");
        entity.Property(e => e.TaskCreatorName).HasColumnName("task_creator_name");

        entity.Property(e => e.TaskAssignedEmployeeId).HasColumnName("task_assigned_employee_id");
        entity.Property(e => e.TaskAssignedEmployeeName).HasColumnName("task_assigned_employee_name");
        entity.Property(e => e.TaskAssignedTeamId).HasColumnName("task_assigned_team_id");
        entity.Property(e => e.TaskAssignedTeamName).HasColumnName("task_assigned_team_name");
    }
}
