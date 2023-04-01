using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.DB.Migrations
{
    /// <inheritdoc />
    public partial class ModifyTaskAssignmentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "task_assignment_unique_index",
                table: "tasks_assignments");

            migrationBuilder.CreateIndex(
                name: "task_assignment_unique_index",
                table: "tasks_assignments",
                columns: new[] { "task_id", "assigned_by", "assigned_to", "assignee_type" },
                unique: true);

            migrationBuilder.DropIndex(
                name: "tasks_assignments_assigned_to_fk_index",
                table: "tasks_assignments");

            migrationBuilder.DropForeignKey(
                name: "employees_tasks_assignments_fk",
                table: "tasks_assignments");

            migrationBuilder.DropForeignKey(
                name: "teams_tasks_assignments_fk",
                table: "tasks_assignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "task_assignment_unique_index",
                table: "tasks_assignments");

            migrationBuilder.CreateIndex(
                name: "task_assignment_unique_index",
                table: "tasks_assignments",
                columns: new[] { "task_id", "assigned_by", "assigned_to" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tasks_assignments_assigned_to_fk_index",
                table: "tasks_assignments",
                column: "assigned_to");

            migrationBuilder.AddForeignKey(
                name: "employees_tasks_assignments_fk",
                table: "tasks_assignments",
                column: "assigned_to",
                principalTable: "employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "teams_tasks_assignments_fk",
                table: "tasks_assignments",
                column: "assigned_to",
                principalTable: "teams",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
