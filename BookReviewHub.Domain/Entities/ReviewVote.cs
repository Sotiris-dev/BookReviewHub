namespace BookReviewHub.Domain.Entities;

public class ReviewVote
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ReviewId { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsUpvote { get; private set; }

    public ReviewVote(Guid reviewId, Guid userId, bool isUpvote)
    {
        ReviewId = reviewId;
        UserId = userId;
        IsUpvote = isUpvote;
    }
}
