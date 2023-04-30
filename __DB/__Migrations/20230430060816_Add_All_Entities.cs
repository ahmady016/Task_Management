using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    manager_id = table.Column<Guid>(name: "manager_id", type: "uniqueidentifier", nullable: true),
                    department_id = table.Column<Guid>(name: "department_id", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    full_name = table.Column<string>(name: "full_name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    job_title = table.Column<string>(name: "job_title", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "FullTime"),
                    gender = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false, defaultValue: "Male"),
                    marital_status = table.Column<string>(name: "marital_status", type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "Single"),
                    photo_url = table.Column<string>(name: "photo_url", type: "varchar(200)", maxLength: 200, nullable: true),
                    department_id = table.Column<Guid>(name: "department_id", type: "uniqueidentifier", nullable: true),
                    manager_id = table.Column<Guid>(name: "manager_id", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                    table.ForeignKey(
                        name: "departments_employees_fk",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "labels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    color_name = table.Column<string>(name: "color_name", type: "varchar(50)", maxLength: 50, nullable: true),
                    color_code = table.Column<string>(name: "color_code", type: "varchar(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(name: "created_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    created_by = table.Column<Guid>(name: "created_by", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labels", x => x.id);
                    table.ForeignKey(
                        name: "employees_labels_fk",
                        column: x => x.created_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "WaterFall"),
                    status = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "Waiting"),
                    created_at = table.Column<DateTime>(name: "created_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    completedat = table.Column<DateTime>(name: "completed_at", type: "datetime2(3)", nullable: true),
                    created_by = table.Column<Guid>(name: "created_by", type: "uniqueidentifier", nullable: false),
                    manageby = table.Column<Guid>(name: "manage_by", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "employees_created_projects_fk",
                        column: x => x.created_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "employees_managed_projects_fk",
                        column: x => x.manageby,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    created_at = table.Column<DateTime>(name: "created_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    created_by = table.Column<Guid>(name: "created_by", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                    table.ForeignKey(
                        name: "employees_created_teams_fk",
                        column: x => x.created_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    priority = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "Normal"),
                    created_at = table.Column<DateTime>(name: "created_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    due_date = table.Column<DateTime>(name: "due_date", type: "datetime2(3)", nullable: true),
                    completed_at = table.Column<DateTime>(name: "completed_at", type: "datetime2(3)", nullable: true),
                    project_id = table.Column<Guid>(name: "project_id", type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<Guid>(name: "created_by", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.id);
                    table.ForeignKey(
                        name: "employees_created_tasks_fk",
                        column: x => x.created_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "projects_tasks_fk",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "teams_members",
                columns: table => new
                {
                    team_id = table.Column<Guid>(name: "team_id", type: "uniqueidentifier", nullable: false),
                    employee_id = table.Column<Guid>(name: "employee_id", type: "uniqueidentifier", nullable: false),
                    created_at = table.Column<DateTime>(name: "created_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams_members", x => new { x.team_id, x.employee_id });
                    table.ForeignKey(
                        name: "employees_teams_members_fk",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "teams_teams_members_fk",
                        column: x => x.team_id,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_actions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    date = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    taken_by = table.Column<Guid>(name: "taken_by", type: "uniqueidentifier", nullable: false),
                    task_id = table.Column<Guid>(name: "task_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_actions", x => x.id);
                    table.ForeignKey(
                        name: "employees_tasks_actions_fk",
                        column: x => x.taken_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "tasks_tasks_actions_fk",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_assignments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    task_id = table.Column<Guid>(name: "task_id", type: "uniqueidentifier", nullable: false),
                    assigned_by = table.Column<Guid>(name: "assigned_by", type: "uniqueidentifier", nullable: false),
                    assigned_to = table.Column<Guid>(name: "assigned_to", type: "uniqueidentifier", nullable: false),
                    assignee_type = table.Column<string>(name: "assignee_type", type: "varchar(30)", maxLength: 30, nullable: false),
                    assigned_at = table.Column<DateTime>(name: "assigned_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    left_at = table.Column<DateTime>(name: "left_at", type: "datetime2(3)", nullable: true),
                    leave_reason = table.Column<string>(name: "leave_reason", type: "nvarchar(500)", maxLength: 500, nullable: true),
                    assigned_employee_id = table.Column<Guid>(name: "assigned_employee_id", type: "uniqueidentifier", nullable: true, computedColumnSql: "CASE WHEN [assignee_type] = 'Employee' THEN [assigned_to] ELSE NULL END", stored: true),
                    assigned_team_id = table.Column<Guid>(name: "assigned_team_id", type: "uniqueidentifier", nullable: true, computedColumnSql: "CASE WHEN [assignee_type] = 'Team' THEN [assigned_to] ELSE NULL END", stored: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_assignments", x => x.id);
                    table.ForeignKey(
                        name: "assigners_tasks_assignments_fk",
                        column: x => x.assigned_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "employees_tasks_assignments_fk",
                        column: x => x.assigned_employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "tasks_tasks_assignments_fk",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "teams_tasks_assignments_fk",
                        column: x => x.assigned_team_id,
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tasks_attachments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    file_url = table.Column<string>(name: "file_url", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    created_at = table.Column<DateTime>(name: "created_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    created_by = table.Column<Guid>(name: "created_by", type: "uniqueidentifier", nullable: false),
                    task_id = table.Column<Guid>(name: "task_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_attachments", x => x.id);
                    table.ForeignKey(
                        name: "employees_tasks_attachments_fk",
                        column: x => x.created_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "tasks_tasks_attachments_fk",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_comments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    commented_at = table.Column<DateTime>(name: "commented_at", type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    commented_by = table.Column<Guid>(name: "commented_by", type: "uniqueidentifier", nullable: false),
                    task_id = table.Column<Guid>(name: "task_id", type: "uniqueidentifier", nullable: false),
                    comment_id = table.Column<Guid>(name: "comment_id", type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_comments", x => x.id);
                    table.ForeignKey(
                        name: "employees_tasks_comments_fk",
                        column: x => x.commented_by,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "tasks_tasks_comments_fk",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_labels",
                columns: table => new
                {
                    task_id = table.Column<Guid>(name: "task_id", type: "uniqueidentifier", nullable: false),
                    label_id = table.Column<Guid>(name: "label_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_labels", x => new { x.task_id, x.label_id });
                    table.ForeignKey(
                        name: "labels_tasks_labels_fk",
                        column: x => x.label_id,
                        principalTable: "labels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "tasks_tasks_labels_fk",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks_states",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    state = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, defaultValue: "Pending"),
                    from = table.Column<DateTime>(type: "datetime2(3)", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    to = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    task_id = table.Column<Guid>(name: "task_id", type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks_states", x => x.id);
                    table.ForeignKey(
                        name: "tasks_tasks_states_fk",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "departments_title_unique_index",
                table: "departments",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "employees_department_id_fk_index",
                table: "employees",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "labels_created_by_fk_index",
                table: "labels",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "labels_title_unique_index",
                table: "labels",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "employees_projects_created_by_fk_index",
                table: "projects",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "employees_projects_manage_by_fk_index",
                table: "projects",
                column: "manage_by");

            migrationBuilder.CreateIndex(
                name: "projects_title_unique_index",
                table: "projects",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tasks_created_by_fk_index",
                table: "tasks",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "tasks_project_id_fk_index",
                table: "tasks",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "tasks_title_unique_index",
                table: "tasks",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tasks_actions_taken_by_fk_index",
                table: "tasks_actions",
                column: "taken_by");

            migrationBuilder.CreateIndex(
                name: "tasks_actions_task_id_fk_index",
                table: "tasks_actions",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "task_assignment_unique_index",
                table: "tasks_assignments",
                columns: new[] { "task_id", "assigned_by", "assigned_to", "assignee_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_by_fk_index",
                table: "tasks_assignments",
                column: "assigned_by");

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_to_employee_fk_index",
                table: "tasks_assignments",
                column: "assigned_employee_id");

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_to_index",
                table: "tasks_assignments",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_to_team_fk_index",
                table: "tasks_assignments",
                column: "assigned_team_id");

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_task_id_fk_index",
                table: "tasks_assignments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "tasks_attachments_created_by_fk_index",
                table: "tasks_attachments",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "tasks_attachments_task_id_fk_index",
                table: "tasks_attachments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "tasks_comments_commented_by_fk_index",
                table: "tasks_comments",
                column: "commented_by");

            migrationBuilder.CreateIndex(
                name: "tasks_comments_task_id_fk_index",
                table: "tasks_comments",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "tasks_labels_label_id_fk_index",
                table: "tasks_labels",
                column: "label_id");

            migrationBuilder.CreateIndex(
                name: "tasks_labels_task_id_fk_index",
                table: "tasks_labels",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "tasks_states_task_id_fk_index",
                table: "tasks_states",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "teams_created_by_fk_index",
                table: "teams",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "teams_name_unique_index",
                table: "teams",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "teams_members_employee_id_fk_index",
                table: "teams_members",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "teams_members_team_id_fk_index",
                table: "teams_members",
                column: "team_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "tasks_comments");
            migrationBuilder.DropTable(name: "tasks_attachments");
            migrationBuilder.DropTable(name: "tasks_actions");
            migrationBuilder.DropTable(name: "tasks_assignments");
            migrationBuilder.DropTable(name: "tasks_labels");
            migrationBuilder.DropTable(name: "tasks_states");
            migrationBuilder.DropTable(name: "teams_members");
            migrationBuilder.DropTable(name: "tasks");
            migrationBuilder.DropTable(name: "labels");
            migrationBuilder.DropTable(name: "projects");
            migrationBuilder.DropTable(name: "teams");
            migrationBuilder.DropTable(name: "employees");
            migrationBuilder.DropTable(name: "departments");
        }
    }
}
