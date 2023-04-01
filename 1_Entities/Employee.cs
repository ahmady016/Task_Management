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
    public EmployeeTypes Type { get; set; } = EmployeeTypes.FullTime;
    public Gender Gender { get; set; } = Gender.Male;
    public MaritalStatuses MaritalStatus { get; set; } = MaritalStatuses.Single;
    public string PhotoUrl { get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? ManagerId { get; set; }

    public virtual Department Department { get; set; }
    public virtual ICollection<Project> CreatedProject { get; set; } = new HashSet<Project>();
    public virtual ICollection<Project> ManagedProject { get; set; } = new HashSet<Project>();
    public virtual ICollection<AppTask> CreatedTasks { get; set; } = new HashSet<AppTask>();
    public virtual ICollection<TaskAssignment> AssignerTasks { get; set; } = new HashSet<TaskAssignment>();
    public virtual ICollection<TaskAssignment> AssignedTasks { get; set; } = new HashSet<TaskAssignment>();
    public virtual ICollection<TaskAction> Actions { get; set; } = new HashSet<TaskAction>();
    public virtual ICollection<TaskComment> Comments { get; set; } = new HashSet<TaskComment>();
    public virtual ICollection<TaskAttachment> AttachedFiles { get; set; } = new HashSet<TaskAttachment>();
    public virtual ICollection<AppLabel> CreatedLabels { get; set; } = new HashSet<AppLabel>();
    public virtual ICollection<Team> CreatedTeams { get; set; } = new HashSet<Team>();
    public virtual ICollection<TeamMember> JoinedTeams { get; set; } = new HashSet<TeamMember>();
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

        entity.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("type")
            .HasColumnType("varchar(10)")
            .HasDefaultValue(EmployeeTypes.FullTime)
            .HasConversion(value => value.ToString(), value => Enum.Parse<EmployeeTypes>(value));

        entity.Property(e => e.Gender)
            .IsRequired()
            .HasMaxLength(6)
            .HasColumnName("gender")
            .HasColumnType("varchar(6)")
            .HasDefaultValue(Gender.Male)
            .HasConversion(value => value.ToString(), value => Enum.Parse<Gender>(value));

        entity.Property(e => e.MaritalStatus)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("marital_status")
            .HasColumnType("varchar(10)")
            .HasDefaultValue(MaritalStatuses.Single)
            .HasConversion(value => value.ToString(), value => Enum.Parse<MaritalStatuses>(value));

        entity.Property(e => e.PhotoUrl)
            .HasMaxLength(200)
            .HasColumnName("photo_url")
            .HasColumnType("varchar(200)");

        entity.Property(e => e.DepartmentId)
            .HasColumnName("department_id")
            .HasColumnType("uniqueidentifier");

        entity.Property(e => e.ManagerId)
            .HasColumnName("manager_id")
            .HasColumnType("uniqueidentifier");

        entity.HasIndex(e => e.DepartmentId, "employees_department_id_fk_index");
        entity.HasOne(employee => employee.Department)
            .WithMany(orgHierarchy => orgHierarchy.Employees)
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("departments_employees_fk");

    }
}

public class EmployeeFaker : Faker<Employee>
{
    public EmployeeFaker()
    {
        RuleFor(o => o.FullName, f => f.Name.FullName(f.PickRandom<Bogus.DataSets.Name.Gender>()));
        RuleFor(o => o.Email, f => f.Internet.Email());
        RuleFor(o => o.JobTitle, f => f.Name.JobTitle());
        RuleFor(o => o.Type, f => f.PickRandom<EmployeeTypes>());
        RuleFor(o => o.Gender, f => f.PickRandom<Gender>());
        RuleFor(o => o.MaritalStatus, f => f.PickRandom<MaritalStatuses>());
        RuleFor(o => o.PhotoUrl, f => f.Image.PicsumUrl());
    }
}
