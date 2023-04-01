using Microsoft.EntityFrameworkCore;
using Bogus;

using TaskManagement.Common;
using TaskManagement.Entities;

namespace TaskManagement.DB;

public static class DBSeeder
{
    private static ILogger<TaskManagementContext> _logger;
    private static TaskManagementContext _db;

    private static Faker _faker = new("en");
    private static DepartmentFaker _departmentFaker = new();
    private static EmployeeFaker _employeeFaker = new();
    private static TeamFaker _teamFaker = new();
    private static ProjectFaker _projectFaker = new();
    private static AppTaskFaker _taskFaker = new();
    private static AppLabelFaker _labelFaker = new();
    private static TaskStateFaker _taskStateFaker = new();
    private static TaskAssignmentFaker _taskAssignmentFaker = new();
    private static TaskActionFaker _taskActionFaker = new();
    private static TaskAttachmentFaker _taskAttachmentFaker = new();
    private static TaskCommentFaker _taskCommentFaker = new();

    private static List<Department> _departments;
    private static List<Employee> _employees;
    private static List<Team> _teams;
    private static List<TeamMember> _teamsMembers;
    private static List<Project> _projects;
    private static List<AppTask> _tasks = new();
    private static List<TaskState> _taskStates = new();
    private static List<AppLabel> _labels;
    private static List<TaskLabel> _tasksLabels;
    private static List<TaskAssignment> _taskAssignments;
    private static List<TaskAction> _taskActions;
    private static List<TaskAttachment> _taskAttachments;
    private static List<TaskComment> _taskComments;

    private static async Task Seed()
    {
        await _db.Database.MigrateAsync();

        #region Fill departments and employees:
        if(_db.Departments.Any() is false && _db.Employees.Any() is false)
        {
            // Fill departments and employees
            _departments = _departmentFaker.Generate(25);
            _employees = _employeeFaker.Generate(200);
            _db.Departments.AddRange(_departments);
            _db.Employees.AddRange(_employees);

            _logger.LogInformation($"Begin Seeding {_departments.Count} departments and {_employees.Count} employees");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding {_departments.Count} departments and {_employees.Count} employees");

            // set each department reference DepartmentId
            var _index = 0; var _item = 1; var _step = _faker.Random.Byte(3, 5);
            while (_item <= _departments.Count)
            {
                foreach (var department in _departments.Skip(_item).Take(_step))
                    department.DepartmentId = _departments[_index].Id;
                _step = _faker.Random.Byte(3, 5);
                _item += _step + 1;
                _index += _step + 1;
            }
            // set foreach department a manager and foreach employee a department and a manager
            _employees.ForEach(employee => employee.DepartmentId = _faker.Random.ListItem(_departments).Id);
            Guid _managerId = Guid.Empty;
            foreach (var department in _departments)
            {
                _managerId = _faker.Random.ListItem(_employees).Id;
                department.ManagerId = _managerId;
                foreach (var employee in _employees.Where(e => e.DepartmentId == department.Id))
                    employee.ManagerId = _managerId;
            }
            _db.Departments.UpdateRange(_departments);
            _db.Employees.UpdateRange(_employees);

            _logger.LogInformation($"Begin Setting departments Hierarchies and manager and Seeding employees department and manager");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Setting departments Hierarchies and manager and Seeding employees department and manager");
        }
        else
        {
            _departments = await _db.Departments.ToListAsync();
            _employees = await _db.Employees.ToListAsync();
        }
        #endregion

        #region Fill Teams and TeamsMembers:
        if(_db.Teams.Any() is false && _db.TeamsMembers.Any() is false)
        {
            // Fill teams and team members
            _teams = _teamFaker.Generate(50);
            var index = 0; var chunk = 4;
            foreach (var team in _teams)
            {
                team.CreatedBy = _faker.PickRandom<Employee>(_employees).Id;
                var _members = _employees.Skip(chunk * index++).Take(chunk).ToList();
                foreach (var member in _members)
                {
                    team.Members.Add(new TeamMember()
                    {
                        TeamId = team.Id,
                        EmployeeId = member.Id,
                        JoinedAt = _faker.Date.Between(DateTime.UtcNow.AddMonths(-3), DateTime.UtcNow.AddMonths(4))
                    });
                }
            }
            _db.Teams.AddRange(_teams);

            _logger.LogInformation($"Begin Seeding {_teams.Count} teams with team members");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding {_teams.Count} teams with team members");
        }
        else
        {
            _teams = await _db.Teams.ToListAsync();
            _teamsMembers = await _db.TeamsMembers.ToListAsync();
        }
        #endregion

        #region Fill Projects, Tasks and Tasks States:
        if(_db.Projects.Any() is false && _db.Tasks.Any() is false)
        {
            // Fill projects and tasks
            _projects = _projectFaker.Generate(50);
            foreach (var project in _projects)
            {
                _tasks.AddRange(_taskFaker.GenerateBetween(10, 20));
                foreach (var task in _tasks)
                {
                    task.States = _taskStateFaker.GenerateBetween(1, 5);
                    _taskStates.AddRange(task.States);
                }
                project.Tasks = _tasks;
                project.CreatedBy = _faker.PickRandom<Employee>(_employees).Id;
            }
            _db.Projects.AddRange(_projects);

            _logger.LogInformation($"Begin Seeding {_projects.Count} projects and {_tasks.Count} tasks");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding {_projects.Count} projects and {_tasks.Count} tasks");
        }
        else
        {
            _projects = await _db.Projects.ToListAsync();
            _tasks = await _db.Tasks.ToListAsync();
        }
        #endregion

        #region Fill Labels and TasksLabels
        if(_db.Labels.Any() is false && _db.TasksLabels.Any() is false)
        {
            // Fill labels and tasks labels
            _labels = _labelFaker.Generate(50);
            var index = 0; var chunk = 4;
            foreach (var label in _labels)
            {
                label.CreatedBy = _faker.PickRandom<Employee>(_employees).Id;
                var _labelTasks = _tasks.Skip(chunk * index++).Take(chunk).ToList();
                foreach (var task in _labelTasks)
                    label.Tasks.Add(new TaskLabel() { LabelId = label.Id, TaskId = task.Id });
            }
            _db.Labels.AddRange(_labels);

            _logger.LogInformation($"Begin Seeding {_labels.Count} Labels with Tasks Labels");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding {_labels.Count} Labels with Tasks Labels");
        }
        else
        {
            _labels = await _db.Labels.ToListAsync();
            _tasksLabels = await _db.TasksLabels.ToListAsync();
        }
        #endregion

        #region Fill Tasks Assignments, Actions, Attachments and Comments:
        if(_db.Assignments.Any() is false)
        {
            _taskAssignments = _taskAssignmentFaker.Generate(1000);
            foreach (var assignment in _taskAssignments)
            {
                assignment.TaskId = _faker.PickRandom<AppTask>(_tasks).Id;
                assignment.AssignedBy = _faker.PickRandom<Employee>(_employees).Id;
                assignment.AssigneeType = _faker.PickRandom<AssigneeTypes>();
                assignment.AssignedTo = (assignment.AssigneeType == AssigneeTypes.Employee)
                    ? _faker.PickRandom<Employee>(_employees).Id
                    : _faker.PickRandom<Team>(_teams).Id;
            }
            _db.Assignments.AddRange(_taskAssignments);

            _logger.LogInformation($"Begin Seeding Tasks Assignments({_taskAssignments.Count})");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding Tasks Assignments({_taskAssignments.Count})");
        }
        if(_db.Actions.Any() is false)
        {
            _taskActions = _taskActionFaker.Generate(2000);
            foreach (var action in _taskActions)
            {
                action.TaskId = _faker.PickRandom<AppTask>(_tasks).Id;
                action.TakenBy = _faker.PickRandom<Employee>(_employees).Id;
            }
            _db.Actions.AddRange(_taskActions);

            _logger.LogInformation($"Begin Seeding Tasks Actions({_taskActions.Count})");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding Tasks Actions({_taskActions.Count})");
        }
        if(_db.Attachments.Any() is false)
        {
            _taskAttachments = _taskAttachmentFaker.Generate(1500);
            foreach (var attachment in _taskAttachments)
            {
                attachment.TaskId = _faker.PickRandom<AppTask>(_tasks).Id;
                attachment.AttachedBy = _faker.PickRandom<Employee>(_employees).Id;
            }
            _db.Attachments.AddRange(_taskAttachments);

            _logger.LogInformation($"Begin Seeding Tasks Attachments({_taskAttachments.Count})");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding Tasks Attachments({_taskAttachments.Count})");
        }
        if(_db.Comments.Any() is false)
        {
            _taskComments = _taskCommentFaker.Generate(2000);
            foreach (var comment in _taskComments)
            {
                comment.TaskId = _faker.PickRandom<AppTask>(_tasks).Id;
                comment.CommentedBy = _faker.PickRandom<Employee>(_employees).Id;
            }
            _db.Comments.AddRange(_taskComments);

            _logger.LogInformation($"Begin Seeding Tasks Comments({_taskComments.Count})");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Seeding Tasks Comments({_taskComments.Count})");
        }
        else
        {
            _taskComments = await _db.Comments.ToListAsync();
        }
        #endregion

        #region Set Comments reference CommentId:
        if(_db.Comments.Any() is true)
        {
            var _index = 0; var _item = 1; var _step = _faker.Random.Byte(3, 9);
            while (_item <= _taskComments.Count)
            {
                foreach (var comment in _taskComments.Skip(_item).Take(_step))
                    comment.CommentId = _taskComments[_index].Id;
                _step = _faker.Random.Byte(3, 9);
                _item += _step + 1;
                _index += _step + 1;
            }
            _db.Comments.UpdateRange(_taskComments);

            _logger.LogInformation($"Begin Updating Comments({_taskComments.Count}) By Setting reference CommentId");
            await _db.SaveChangesAsync();
            _logger.LogInformation($"Done Updating Comments({_taskComments.Count}) By Setting reference CommentId");
        }
        #endregion
    }
    public static async Task UseDbSeeder(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));
        using var scope = app.ApplicationServices.CreateScope();
        try
        {
            _logger = scope.ServiceProvider.GetRequiredService<ILogger<TaskManagementContext>>();
            _db = scope.ServiceProvider.GetRequiredService<TaskManagementContext>();
            await Seed();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }
}
