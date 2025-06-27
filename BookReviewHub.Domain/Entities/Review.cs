namespace BookReviewHub.Domain.Entities;

public class Review
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Content { get; private set; } = default!;
    public int Rating { get; private set; }
    public DateTime DateCreated { get; private set; } = DateTime.UtcNow;

    public Guid BookId { get; private set; }
    public Guid UserId { get; private set; }

    // Navigation properties
    public Book Book { get; private set; } = default!;
    public User User { get; private set; } = default!;

    private Review() { }

    public Review(Guid bookId, Guid userId, string content, int rating)
    {
        BookId = bookId;
        UserId = userId;
        Content = content;
        Rating = rating;
        Rating = rating is < 1 or > 5
            ? throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5")
            : rating;
    }
}
