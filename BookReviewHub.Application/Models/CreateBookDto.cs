namespace BookReviewHub.Application.Models
{
    public record CreateBookDto(string Title, string Author, int PublishedYear, string Genre);
}
