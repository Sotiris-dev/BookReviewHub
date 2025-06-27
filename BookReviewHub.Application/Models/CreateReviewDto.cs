namespace BookReviewHub.Application.Models
{
    public record CreateReviewDto(Guid BookId, Guid UserId, string Content, int Rating);
}
