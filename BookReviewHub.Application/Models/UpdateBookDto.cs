namespace BookReviewHub.Application.Models
{
    public record UpdateBookDto(Guid Id, string Title, string Author, int PublishedYear, string Genre);
}
