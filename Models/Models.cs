namespace WebWorkshop.Models;

public enum ProjectStatus { Planned = 0, InProgress = 1, Completed = 2, Cancelled = 3 }
public enum WorkTaskStatus { Open = 0, InProgress = 1, Done = 2, Cancelled = 3 }

public sealed class CustomerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string PassportOrIdNumber { get; set; } = "";
}

public sealed class PerformerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Position { get; set; } = "";
}

public sealed class ProjectDto
{
    public int Id { get; set; }
    public string Code { get; set; } = "";
    public string Name { get; set; } = "";
    public string Status { get; set; } = "";

    public int CustomerId { get; set; }
}

public sealed class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Status { get; set; } = "";
    public DateTime DueDateUtc { get; set; }
    public decimal Cost { get; set; }
    public int ProjectId { get; set; }
    public int PerformerId { get; set; }
}
