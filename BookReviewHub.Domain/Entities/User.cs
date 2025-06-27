namespace BookReviewHub.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Username { get; private set; } = default!;
    public string Email { get; private set; } = default!;

    private User() { } // EF Core

    public User(string username, string email)
    {
        Username = username;
        Email = email;
    }
}
