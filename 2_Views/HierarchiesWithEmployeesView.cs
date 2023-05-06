using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskManagement.Views;

public class HierarchiesWithEmployeesView
{
    public Guid? BossId { get; set; }
    public string BossTitle { get; set; }
    public Guid? BossManagerId { get; set; }
    public string BossManagerName { get; set; }
    public Guid DepartmentId { get; set; }
    public string DepartmentTitle { get; set; }
    public Guid? DepartmentManagerId { get; set; }
    public string DepartmentManagerName { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeEmail { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeJob { get; set; }
    public string EmployeePhoto { get; set; }
}

public class HierarchiesWithEmployeesViewConfig : IEntityTypeConfiguration<HierarchiesWithEmployeesView>
{
    public void Configure(EntityTypeBuilder<HierarchiesWithEmployeesView> entity)
    {
        entity.ToView("hierarchies_with_employees_view");
        entity.HasNoKey();

        entity.Property(e => e.BossId).HasColumnName("boss_id");
        entity.Property(e => e.BossTitle).HasColumnName("boss_title");
        entity.Property(e => e.BossManagerId).HasColumnName("boss_manager_id");
        entity.Property(e => e.BossManagerName).HasColumnName("boss_manager_name");

        entity.Property(e => e.DepartmentId).HasColumnName("department_id");
        entity.Property(e => e.DepartmentTitle).HasColumnName("department_title");
        entity.Property(e => e.DepartmentManagerId).HasColumnName("department_manager_id");
        entity.Property(e => e.DepartmentManagerName).HasColumnName("department_manager_name");

        entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
        entity.Property(e => e.EmployeeEmail).HasColumnName("employee_email");
        entity.Property(e => e.EmployeeName).HasColumnName("employee_name");
        entity.Property(e => e.EmployeeJob).HasColumnName("employee_job");
        entity.Property(e => e.EmployeePhoto).HasColumnName("employee_photo");
    }
}
