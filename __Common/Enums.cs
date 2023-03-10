namespace TaskManagement.Common;

public enum Gender : byte
{
    Male = 1,
    Female = 2
}

public enum MaritalStatuses : byte
{
    Single = 1,
    Married = 2,
    Divorced = 3,
    widowed = 4
}

public enum EmployeeTypes : byte
{
    FullTime = 1,
    PartTime = 2,
    Temporary = 3,
    Seasonal = 4,
    internship = 5,
    Leased = 6
}

public enum ProjectTypes : byte
{
    WaterFall = 1,
    Lean = 2,
    Agile = 3,
    Kanban = 4,
    Scrum = 5
}

public enum TaskPriorities : byte
{
    Low = 1,
    Normal = 2,
    High = 3,
    RealTime = 4
}

public enum TaskStatuses : byte
{
    Pending = 1,
    InProgress = 2,
    Paused = 3,
    Canceled = 4,
    Completed = 5
}
