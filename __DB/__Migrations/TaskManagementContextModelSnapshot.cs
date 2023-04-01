﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.DB;

#nullable disable

namespace TaskManagement.DB.Migrations
{
    [DbContext(typeof(TaskManagementContext))]
    partial class TaskManagementContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TaskManagement.Entities.AppLabel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<string>("ColorCode")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("color_code");

                    b.Property<string>("ColorName")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("color_name");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("labels_title_unique_index");

                    b.HasIndex(new[] { "CreatedBy" }, "labels_created_by_fk_index");

                    b.ToTable("labels", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.AppTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("completed_at");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("description");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("due_date");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("Normal")
                        .HasColumnName("priority_id");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("project_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("tasks_title_unique_index");

                    b.HasIndex(new[] { "CreatedBy" }, "tasks_created_by_fk_index");

                    b.HasIndex(new[] { "ProjectId" }, "tasks_project_id_fk_index");

                    b.ToTable("tasks", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("department_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("description");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("manager_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("departments_title_unique_index");

                    b.ToTable("departments", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("department_id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("full_name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(6)
                        .HasColumnType("varchar(6)")
                        .HasDefaultValue("Male")
                        .HasColumnName("gender");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("job_title");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("manager_id");

                    b.Property<string>("MaritalStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("Single")
                        .HasColumnName("marital_status");

                    b.Property<string>("PhotoUrl")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("photo_url");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("FullTime")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "DepartmentId" }, "employees_department_id_fk_index");

                    b.ToTable("employees", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime?>("CompletedAt")
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("completed_at");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("description");

                    b.Property<Guid?>("ManageBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("manage_by");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("Waiting")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.Property<string>("Type")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("WaterFall")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique()
                        .HasDatabaseName("projects_title_unique_index");

                    b.HasIndex(new[] { "CreatedBy" }, "employees_projects_created_by_fk_index");

                    b.HasIndex(new[] { "ManageBy" }, "employees_projects_manage_by_fk_index");

                    b.ToTable("projects", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskAction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<Guid>("TakenBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("taken_by");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("task_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TakenBy" }, "tasks_actions_taken_by_fk_index");

                    b.HasIndex(new[] { "TaskId" }, "tasks_actions_task_id_fk_index");

                    b.ToTable("tasks_actions", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskAssignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("AssignedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("assigned_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<Guid>("AssignedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("assigned_by");

                    b.Property<Guid>("AssignedTo")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("assigned_to");

                    b.Property<string>("AssigneeType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("assignee_type");

                    b.Property<string>("LeaveReason")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("leave_reason");

                    b.Property<DateTime?>("LeftAt")
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("left_at");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("task_id");

                    b.HasKey("Id");

                    b.HasIndex("TaskId", "AssignedBy", "AssignedTo")
                        .IsUnique()
                        .HasDatabaseName("task_assignment_unique_index");

                    b.HasIndex(new[] { "AssignedBy" }, "tasks_assignments_assigned_by_fk_index");

                    b.HasIndex(new[] { "AssignedTo" }, "tasks_assignments_assigned_to_employee_fk_index");

                    b.HasIndex(new[] { "AssignedTo" }, "tasks_assignments_assigned_to_team_fk_index");

                    b.HasIndex(new[] { "TaskId" }, "tasks_assignments_task_id_fk_index");

                    b.ToTable("tasks_assignments", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("AttachedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("file_url");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("task_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "AttachedBy" }, "tasks_attachments_created_by_fk_index");

                    b.HasIndex(new[] { "TaskId" }, "tasks_attachments_task_id_fk_index");

                    b.ToTable("tasks_attachments", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid?>("CommentId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("comment_id");

                    b.Property<DateTime>("CommentedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("commented_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<Guid>("CommentedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("commented_by");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("task_id");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("text");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CommentedBy" }, "tasks_comments_commented_by_fk_index");

                    b.HasIndex(new[] { "TaskId" }, "tasks_comments_task_id_fk_index");

                    b.ToTable("tasks_comments", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskLabel", b =>
                {
                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("task_id");

                    b.Property<Guid>("LabelId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("label_id");

                    b.HasKey("TaskId", "LabelId");

                    b.HasIndex(new[] { "LabelId" }, "tasks_labels_label_id_fk_index");

                    b.HasIndex(new[] { "TaskId" }, "tasks_labels_task_id_fk_index");

                    b.ToTable("tasks_labels", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskState", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("From")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("from")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<string>("State")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasDefaultValue("Pending")
                        .HasColumnName("state");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("task_id");

                    b.Property<DateTime?>("To")
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("to");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TaskId" }, "tasks_states_task_id_fk_index");

                    b.ToTable("tasks_states", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("created_by");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "CreatedBy" }, "teams_created_by_fk_index");

                    b.ToTable("teams", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.TeamMember", b =>
                {
                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("team_id");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("employee_id");

                    b.Property<DateTime>("JoinedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2(3)")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("SYSDATETIME()");

                    b.HasKey("TeamId", "EmployeeId");

                    b.HasIndex(new[] { "EmployeeId" }, "teams_members_employee_id_fk_index");

                    b.HasIndex(new[] { "TeamId" }, "teams_members_team_id_fk_index");

                    b.ToTable("teams_members", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Entities.AppLabel", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Creator")
                        .WithMany("CreatedLabels")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("employees_labels_fk");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("TaskManagement.Entities.AppTask", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Creator")
                        .WithMany("CreatedTasks")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("employees_created_tasks_fk");

                    b.HasOne("TaskManagement.Entities.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("projects_tasks_fk");

                    b.Navigation("Creator");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskManagement.Entities.Employee", b =>
                {
                    b.HasOne("TaskManagement.Entities.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("departments_employees_fk");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("TaskManagement.Entities.Project", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Creator")
                        .WithMany("CreatedProject")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("employees_created_projects_fk");

                    b.HasOne("TaskManagement.Entities.Employee", "Manager")
                        .WithMany("ManagedProject")
                        .HasForeignKey("ManageBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("employees_managed_projects_fk");

                    b.Navigation("Creator");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskAction", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Taker")
                        .WithMany("Actions")
                        .HasForeignKey("TakenBy")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("employees_tasks_actions_fk");

                    b.HasOne("TaskManagement.Entities.AppTask", "Task")
                        .WithMany("Actions")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tasks_tasks_actions_fk");

                    b.Navigation("Taker");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskAssignment", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Assigner")
                        .WithMany("AssignerTasks")
                        .HasForeignKey("AssignedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("assigners_tasks_assignments_fk");

                    b.HasOne("TaskManagement.Entities.Employee", "AssignedEmployee")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignedTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("employees_tasks_assignments_fk");

                    b.HasOne("TaskManagement.Entities.Team", "AssignedTeam")
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignedTo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("teams_tasks_assignments_fk");

                    b.HasOne("TaskManagement.Entities.AppTask", "Task")
                        .WithMany("Assignees")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tasks_tasks_assignments_fk");

                    b.Navigation("AssignedEmployee");

                    b.Navigation("AssignedTeam");

                    b.Navigation("Assigner");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskAttachment", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Attacher")
                        .WithMany("AttachedFiles")
                        .HasForeignKey("AttachedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("employees_tasks_attachments_fk");

                    b.HasOne("TaskManagement.Entities.AppTask", "Task")
                        .WithMany("Attachments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tasks_tasks_attachments_fk");

                    b.Navigation("Attacher");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskComment", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Commenter")
                        .WithMany("Comments")
                        .HasForeignKey("CommentedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("employees_tasks_comments_fk");

                    b.HasOne("TaskManagement.Entities.AppTask", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tasks_tasks_comments_fk");

                    b.Navigation("Commenter");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskLabel", b =>
                {
                    b.HasOne("TaskManagement.Entities.AppLabel", "Label")
                        .WithMany("Tasks")
                        .HasForeignKey("LabelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("labels_tasks_labels_fk");

                    b.HasOne("TaskManagement.Entities.AppTask", "Task")
                        .WithMany("Labels")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tasks_tasks_labels_fk");

                    b.Navigation("Label");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Entities.TaskState", b =>
                {
                    b.HasOne("TaskManagement.Entities.AppTask", "Task")
                        .WithMany("States")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("tasks_tasks_states_fk");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Entities.Team", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Creator")
                        .WithMany("CreatedTeams")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired()
                        .HasConstraintName("employees_created_teams_fk");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("TaskManagement.Entities.TeamMember", b =>
                {
                    b.HasOne("TaskManagement.Entities.Employee", "Employee")
                        .WithMany("JoinedTeams")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("employees_teams_members_fk");

                    b.HasOne("TaskManagement.Entities.Team", "Team")
                        .WithMany("Members")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("teams_teams_members_fk");

                    b.Navigation("Employee");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("TaskManagement.Entities.AppLabel", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManagement.Entities.AppTask", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("Assignees");

                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("Labels");

                    b.Navigation("States");
                });

            modelBuilder.Entity("TaskManagement.Entities.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TaskManagement.Entities.Employee", b =>
                {
                    b.Navigation("Actions");

                    b.Navigation("AssignedTasks");

                    b.Navigation("AssignerTasks");

                    b.Navigation("AttachedFiles");

                    b.Navigation("Comments");

                    b.Navigation("CreatedLabels");

                    b.Navigation("CreatedProject");

                    b.Navigation("CreatedTasks");

                    b.Navigation("CreatedTeams");

                    b.Navigation("JoinedTeams");

                    b.Navigation("ManagedProject");
                });

            modelBuilder.Entity("TaskManagement.Entities.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManagement.Entities.Team", b =>
                {
                    b.Navigation("AssignedTasks");

                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
