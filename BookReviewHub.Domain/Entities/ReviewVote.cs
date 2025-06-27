namespace BookReviewHub.Domain.Entities;

public class ReviewVote
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid ReviewId { get; private set; }

    public Guid UserId { get; private set; }

    public bool IsUpvote { get; private set; }

    /// <summary>
    /// Creates a new review vote (upvote or downvote) from a user for a specific review.
    /// </summary>
    /// <param name="reviewId">The ID of the review being voted on.</param>
    /// <param name="userId">The ID of the user submitting the vote.</param>
    /// <param name="isUpvote">Whether the vote is an upvote (true) or downvote (false).</param>
    public ReviewVote(Guid reviewId, Guid userId, bool isUpvote)
    {
        ReviewId = reviewId;
        UserId = userId;
        IsUpvote = isUpvote;
    }

    /// <summary>
    /// Updates the vote type (upvote/downvote) for an existing review vote.
    /// </summary>
    /// <param name="isUpvote">New vote direction.</param>
    public void UpdateVote(bool isUpvote)
    {
        IsUpvote = isUpvote;
    }
}
