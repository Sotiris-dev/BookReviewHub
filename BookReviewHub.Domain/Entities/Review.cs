using BookReviewHub.Domain.Entities;

public class Review
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public string UserId { get; private set; }
    public string Content { get; private set; }
    public int Rating { get; private set; }
    public DateTime DateCreated { get; private set; }

    public Book Book { get; private set; } = default!;

    public Review(Guid bookId, string userId, string content, int rating)
    {
        Id = Guid.NewGuid();
        BookId = bookId;
        UserId = userId;
        Content = content;
        Rating = rating is < 1 or > 5
            ? throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5")
            : rating;
        DateCreated = DateTime.UtcNow;
    }
}
