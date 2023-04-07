using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.DB.Migrations
{
    /// <inheritdoc />
    public partial class RefactorTaskAssignmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_to_index",
                table: "tasks_assignments",
                column: "assigned_to");

            migrationBuilder.AddColumn<Guid>(
                name: "assigned_employee_id",
                table: "tasks_assignments",
                type: "uniqueidentifier",
                nullable: true,
                computedColumnSql: "CASE WHEN [assignee_type] = 'Employee' THEN [assigned_to] ELSE NULL END",
                stored: true);

            migrationBuilder.AddColumn<Guid>(
                name: "assigned_team_id",
                table: "tasks_assignments",
                type: "uniqueidentifier",
                nullable: true,
                computedColumnSql: "CASE WHEN [assignee_type] = 'Team' THEN [assigned_to] ELSE NULL END",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_to_employee_fk_index",
                table: "tasks_assignments",
                column: "assigned_employee_id");

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_to_team_fk_index",
                table: "tasks_assignments",
                column: "assigned_team_id");

            migrationBuilder.AddForeignKey(
                name: "employees_tasks_assignments_fk",
                table: "tasks_assignments",
                column: "assigned_employee_id",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "teams_tasks_assignments_fk",
                table: "tasks_assignments",
                column: "assigned_team_id",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "tasks_assignments_assigned_to_index",
                table: "tasks_assignments");

            migrationBuilder.DropForeignKey(
                name: "employees_tasks_assignments_fk",
                table: "tasks_assignments");

            migrationBuilder.DropForeignKey(
                name: "teams_tasks_assignments_fk",
                table: "tasks_assignments");

            migrationBuilder.DropIndex(
                name: "tasks_assignments_assigned_to_employee_fk_index",
                table: "tasks_assignments");

            migrationBuilder.DropIndex(
                name: "tasks_assignments_assigned_to_team_fk_index",
                table: "tasks_assignments");

            migrationBuilder.DropColumn(
                name: "assigned_employee_id",
                table: "tasks_assignments");

            migrationBuilder.DropColumn(
                name: "assigned_team_id",
                table: "tasks_assignments");
        }
    }
}
