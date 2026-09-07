namespace TaskFlow.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}