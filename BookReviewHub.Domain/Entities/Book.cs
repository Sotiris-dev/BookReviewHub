namespace BookReviewHub.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = default!;
    public string Author { get; private set; } = default!;
    public int PublishedYear { get; private set; }
    public string Genre { get; private set; } = default!;

    public Book(string title, string author, int publishedYear, string genre)
    {
        Title = title;
        Author = author;
        PublishedYear = publishedYear;
        Genre = genre;
    }
}
