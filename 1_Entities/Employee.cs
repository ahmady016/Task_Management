using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bogus;

using TaskManagement.Common;

namespace TaskManagement.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string JobTitle { get; set; }
    public EmployeeTypes TypeId { get; set; } = EmployeeTypes.FullTime;
    public Gender GenderId { get; set; } = Gender.Male;
    public MaritalStatuses MaritalStatusId { get; set; } = MaritalStatuses.Single;
    public string PhotoUrl { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? ManagerId { get; set; }

    public virtual Department Department { get; set; }
    public virtual ICollection<Project> CreatedProject { get; set; } = new HashSet<Project>();
    public virtual ICollection<Project> ManagedProject { get; set; } = new HashSet<Project>();
    public virtual ICollection<AppTask> CreatedTasks { get; set; } = new HashSet<AppTask>();
    public virtual ICollection<TaskAssignee> AssignerTasks { get; set; } = new HashSet<TaskAssignee>();
    public virtual ICollection<TaskAssignee> AssignedTasks { get; set; } = new HashSet<TaskAssignee>();
    public virtual ICollection<TaskAction> Actions { get; set; } = new HashSet<TaskAction>();
}

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> entity)
    {
        entity.ToTable("employees");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasDefaultValueSql("NEWID()")
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("full_name")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("email")
            .HasColumnType("varchar(100)");

        entity.Property(e => e.JobTitle)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("job_title")
            .HasColumnType("nvarchar(100)");

        entity.Property(e => e.TypeId)
            .IsRequired()
            .HasDefaultValue(EmployeeTypes.FullTime)
            .HasColumnName("type_id")
            .HasColumnType("tinyint");

        entity.Property(e => e.GenderId)
            .IsRequired()
            .HasDefaultValue(Gender.Male)
            .HasColumnName("gender_id")
            .HasColumnType("tinyint");

        entity.Property(e => e.MaritalStatusId)
            .IsRequired()
            .HasDefaultValue(MaritalStatuses.Single)
            .HasColumnName("marital_status_id")
            .HasColumnType("tinyint");

        entity.Property(e => e.PhotoUrl)
            .HasMaxLength(200)
            .HasColumnName("photo_url")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.DepartmentId)
            .HasColumnName("department_id")
            .HasColumnType("bigint");

        entity.Property(e => e.ManagerId)
            .HasColumnName("manager_id")
            .HasColumnType("bigint");

        entity.HasIndex(e => e.DepartmentId, "employees_department_id_fk_index");
        entity.HasOne(employee => employee.Department)
            .WithMany(orgHierarchy => orgHierarchy.Employees)
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("departments_employees_fk");

    }
}

public class EmployeeFaker : Faker<Employee> {
    private short counter = 1;
    public EmployeeFaker()
    {
        RuleFor(o => o.FullName, f => f.Name.FullName(f.PickRandom<Bogus.DataSets.Name.Gender>()));
        RuleFor(o => o.Email, f => f.Internet.Email());
        RuleFor(o => o.JobTitle, f => f.Name.JobTitle());
        RuleFor(o => o.TypeId, f => f.PickRandom<EmployeeTypes>());
        RuleFor(o => o.GenderId, f => f.PickRandom<Gender>());
        RuleFor(o => o.MaritalStatusId, f => f.PickRandom<MaritalStatuses>());
        RuleFor(o => o.PhotoUrl, f => f.Image.PicsumUrl());
    }
}
