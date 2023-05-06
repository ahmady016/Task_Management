using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Views;

public partial class TasksWithAllAssigneesView
{
    public Guid TaskId { get; set; }
    public string TaskTitle { get; set; }
    public DateTime TaskCreatedAt { get; set; }
    public Guid TaskCreatorId { get; set; }
    public string TaskCreatorName { get; set; }
    public Guid AssignmentId { get; set; }
    public DateTime AssignedAt { get; set; }
    public string AssignerName { get; set; }
    public Guid? AssignedEmployeeId { get; set; }
    public string AssignedEmployeeName { get; set; }
    public Guid? AssignedTeamId { get; set; }
    public string AssignedTeamName { get; set; }
    public DateTime? LeftAt { get; set; }
    public string LeaveReason { get; set; }
}

public class TasksWithAllAssigneesViewConfig : IEntityTypeConfiguration<TasksWithAllAssigneesView>
{
    public void Configure(EntityTypeBuilder<TasksWithAllAssigneesView> entity)
    {
        entity.ToView("tasks_with_all_assignees_view");
        entity.HasNoKey();

        entity.Property(e => e.TaskId).HasColumnName("task_id");
        entity.Property(e => e.TaskTitle).HasColumnName("task_title");
        entity.Property(e => e.TaskCreatedAt).HasColumnName("task_created_at");
        entity.Property(e => e.TaskCreatorId).HasColumnName("task_creator_id");
        entity.Property(e => e.TaskCreatorName).HasColumnName("task_creator_name");

        entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
        entity.Property(e => e.AssignedAt).HasColumnName("assigned_at");
        entity.Property(e => e.AssignerName).HasColumnName("assigner_name");
        entity.Property(e => e.AssignedEmployeeId).HasColumnName("assigned_employee_id");
        entity.Property(e => e.AssignedEmployeeName).HasColumnName("assigned_employee_name");
        entity.Property(e => e.AssignedTeamId).HasColumnName("assigned_team_id");
        entity.Property(e => e.AssignedTeamName).HasColumnName("assigned_team_name");
        entity.Property(e => e.LeftAt).HasColumnName("left_at");
        entity.Property(e => e.LeaveReason).HasColumnName("leave_reason");
    }
}
