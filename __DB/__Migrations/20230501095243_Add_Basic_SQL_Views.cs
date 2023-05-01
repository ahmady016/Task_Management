using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddBasicSQLViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW hierarchies_with_employees_view
                AS
                WITH hierarchies AS
                (
                    SELECT b.id AS boss_id,
                        b.title AS boss_title,
                        b.manager_id AS boss_manager_id,
                        bm.full_name AS boss_manager_name,
                        d.id AS department_id,
                        d.title AS department_title,
                        d.manager_id AS department_manager_id,
                        dm.full_name AS department_manager_name
                    FROM departments b
                    JOIN departments d ON b.id = d.department_id
                    JOIN employees bm ON b.manager_id = bm.id
                    JOIN employees dm ON d.manager_id = dm.id
                )
                SELECT h.*,
                    e.id AS employee_id,
                    e.email AS employee_email,
                    e.full_name AS employee_name,
                    e.job_title AS employee_job,
                    e.photo_url AS employee_photo
                FROM hierarchies h JOIN employees e ON h.department_id = e.department_id
                GO
            ");
            migrationBuilder.Sql(@"
                CREATE VIEW hierarchies_tree_view
                AS
                WITH tree_view AS
                (
                    SELECT id,
                        title,
                        department_id,
                        0 AS node_level,
                        CAST(id AS varchar(50)) AS tree_sequence
                    FROM departments WHERE department_id IS NULL
                    UNION ALL
                    SELECT d.id,
                        d.title,
                        d.department_id,
                        node_level + 1 AS node_level,
                        CAST(tree_sequence + '_' + CAST(d.id AS VARCHAR (50)) AS VARCHAR(50)) AS tree_sequence
                    FROM departments d JOIN tree_view t ON t.id = d.department_id
                )
                SELECT	t.id AS department_id,
                    t.title AS department_title,
                    t.node_level,
                    t.tree_sequence,
                    boss.id AS boss_id,
                    boss.title AS boss_title
                FROM tree_view t LEFT JOIN departments boss ON t.department_id = boss.id
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW labels_with_tasks_view
                AS
                SELECT
                    l.id AS label_id,
                    l.title AS label_name,
                    e.id AS label_creator_id,
                    e.full_name AS label_creator_name,
                    tl.task_id AS task_id,
                    t.title AS task_name,
                    t.priority AS task_priority,
                    t.created_at AS task_created_at,
                    t.due_date AS task_due_date,
                    t.completed_at AS task_completed_at,
                    emp.id AS task_creator_id,
                    emp.full_name AS task_creator_name,
                    em.id AS task_assigned_employee_id,
                    em.full_name AS task_assigned_employee_name,
                    tm.id AS task_assigned_team_id,
                    tm.name AS task_assigned_team_name
                FROM labels l
                    JOIN employees e ON e.id = l.created_by
                    JOIN tasks_labels tl ON l.id = tl.label_id
                    JOIN tasks t ON t.id = tl.task_id
                    JOIN employees emp ON emp.id = t.created_by
                    JOIN tasks_assignments ta ON emp.id = ta.assigned_by
                    LEFT JOIN employees em ON em.id = ta.assigned_employee_id
                    LEFT JOIN teams tm ON tm.id = ta.assigned_team_id
                GO
            ");
            migrationBuilder.Sql(@"
                CREATE VIEW projects_with_tasks_view
                AS
                SELECT
                    p.id AS project_id,
                    p.title AS project_title,
                    p.description AS project_description,
                    p.created_at AS project_created_at,
                    p.completed_at AS project_completed_at,
                    e.id AS project_creator_id,
                    e.full_name AS project_creator_name,
                    em.id AS project_manager_id,
                    em.full_name AS project_manager_name,
                    t.id AS task_id,
                    t.title AS task_title,
                    t.description AS task_description,
                    t.priority AS task_priority,
                    t.created_at AS task_created_at,
                    t.completed_at AS task_completed_at,
                    t.due_date AS task_due_date,
                    emp.id AS task_creator_id,
                    emp.full_name AS task_creator_name
                FROM projects p
                    JOIN employees e ON e.id = p.created_by
                    JOIN employees em ON em.id = p.manage_by
                    JOIN tasks t ON p.id = t.project_id
                    JOIN employees emp ON emp.id = t.created_by
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW tasks_with_all_assignees_view
                AS
                WITH _assignments AS
                (
                    SELECT assignments.id,
                        assignments.task_id,
                        assignments.assigned_at,
                        assignments.assigned_by AS assigner_id,
                        assigners.full_name AS assigner_name,
                        assignments.assigned_employee_id,
                        assigned_employee.full_name AS assigned_employee_name,
                        assignments.assigned_team_id,
                        assigned_team.name AS assigned_team_name,
                        assignments.left_at,
                        assignments.leave_reason
                    FROM tasks_assignments assignments
                    JOIN employees assigners                ON assignments.assigned_by = assigners.id
                    LEFT JOIN employees assigned_employee   ON assignments.assigned_employee_id = assigned_employee.id
                    LEFT JOIN teams assigned_team           ON assignments.assigned_team_id = assigned_team.id
                )
                SELECT t.id AS task_id,
                    t.title AS task_title,
                    t.created_at AS task_created_at,
                    e.id AS task_creator_id,
                    e.full_name AS task_creator_name,
                    a.id AS assignment_id,
                    a.assigned_at,
                    a.assigner_name,
                    a.assigned_employee_id,
                    a.assigned_employee_name,
                    a.assigned_team_id,
                    a.assigned_team_name,
                    a.left_at,
                    a.leave_reason
                FROM tasks t
                JOIN _assignments a	ON t.id = a.task_id
                JOIN employees	  e	ON t.created_by = e.id
                GO
            ");
            migrationBuilder.Sql(@"
                CREATE VIEW tasks_with_last_assignee_view
                AS
                WITH _assignments AS
                (
                    SELECT assignments.id,
                        assignments.task_id,
                        assignments.assigned_at,
                        assignments.assigned_by AS assigner_id,
                        assigners.full_name AS assigner_name,
                        assignments.assigned_employee_id,
                        assigned_employee.full_name AS assigned_employee_name,
                        assignments.assigned_team_id,
                        assigned_team.name AS assigned_team_name,
                        assignments.left_at,
                        assignments.leave_reason,
                        ROW_NUMBER() OVER (PARTITION BY assignments.task_id ORDER BY assignments.assigned_at DESC) AS row_id
                    FROM tasks_assignments assignments
                    JOIN employees assigners                ON assignments.assigned_by = assigners.id
                    LEFT JOIN employees assigned_employee   ON assignments.assigned_employee_id = assigned_employee.id
                    LEFT JOIN teams assigned_team           ON assignments.assigned_team_id = assigned_team.id
                )
                SELECT t.id AS task_id,
                    t.title AS task_title,
                    t.created_at AS task_created_at,
                    t.due_date AS task_due_date,
                    t.completed_at AS task_completed_at,
                    t.priority AS task_priority,
                    e.id AS task_creator_id,
                    e.full_name AS task_creator_name,
                    a.id AS assignment_id,
                    a.assigned_at,
                    a.assigner_name,
                    a.assigned_employee_id,
                    a.assigned_employee_name,
                    a.assigned_team_id,
                    a.assigned_team_name,
                    a.left_at,
                    a.leave_reason,
                    a.row_id,
                    f.id AS file_id,
                    f.file_url
                FROM tasks t
                JOIN _assignments       a	ON t.id = a.task_id
                JOIN employees	        e	ON t.created_by = e.id
                JOIN tasks_attachments 	f	ON t.id = f.task_id
                WHERE row_id = 1
                GO
            ");

            migrationBuilder.Sql(@"
                CREATE VIEW projects_tasks_stats_view
                AS
                SELECT
                    project_id,
                    project_title,
                    SUM(CASE WHEN task_completed_at is NULL THEN 0 ELSE 1 END) AS completed,
                    SUM(CASE WHEN task_completed_at is NULL AND task_due_date < SYSDATETIME() THEN 1 ELSE 0 END) AS overdue,
                    SUM(CASE WHEN task_completed_at is NULL AND (task_due_date >= SYSDATETIME() OR task_due_date is NULL) THEN 1 ELSE 0 END) AS running,
                    COUNT(*) AS total,
                    SUM(CASE WHEN task_priority = 'High' OR task_priority = 'RealTime' THEN 1 ELSE 0 END) AS important
                FROM projects_with_tasks_view
                GROUP BY project_id, project_title
                GO
            ");
            migrationBuilder.Sql(@"
                CREATE VIEW assignees_tasks_stats_view
                AS
                SELECT
                    assigned_employee_id,
                    assigned_employee_name,
                    assigned_team_id,
                    assigned_team_name,
                    SUM(CASE WHEN task_completed_at is NULL THEN 0 ELSE 1 END) AS completed,
                    SUM(CASE WHEN task_completed_at is NULL AND task_due_date < SYSDATETIME() THEN 1 ELSE 0 END) AS overdue,
                    SUM(CASE WHEN task_completed_at is NULL AND (task_due_date >= SYSDATETIME() OR task_due_date is NULL) THEN 1 ELSE 0 END) AS running,
                    COUNT(*) AS total,
                    SUM(CASE WHEN task_priority = 'High' OR task_priority = 'RealTime' THEN 1 ELSE 0 END) AS important
                FROM tasks_with_last_assignee_view
                GROUP BY assigned_employee_id, assigned_employee_name, assigned_team_id, assigned_team_name
                GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW hierarchies_with_employees_view");
            migrationBuilder.Sql(@"DROP VIEW hierarchies_tree_view");

            migrationBuilder.Sql(@"DROP VIEW labels_with_tasks_view");
            migrationBuilder.Sql(@"DROP VIEW projects_with_tasks_view");

            migrationBuilder.Sql(@"DROP VIEW tasks_with_all_assignees_view");
            migrationBuilder.Sql(@"DROP VIEW tasks_with_last_assignee_view");

            migrationBuilder.Sql(@"DROP VIEW projects_tasks_stats_view");
            migrationBuilder.Sql(@"DROP VIEW assignees_tasks_stats_view");
        }
    }
}
