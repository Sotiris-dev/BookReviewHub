namespace BookReviewHub.Application.Models
{
    public record ReviewDto(Guid Id, Guid BookId, Guid UserId, string Content, int Rating, DateTime DateCreated);
}
