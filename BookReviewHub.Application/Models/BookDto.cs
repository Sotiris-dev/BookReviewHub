namespace BookReviewHub.Application.Models
{
    public record BookDto(Guid Id, string Title, string Author, int PublishedYear, string Genre);
}
