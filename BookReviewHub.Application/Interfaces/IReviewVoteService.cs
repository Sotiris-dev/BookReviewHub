namespace BookReviewHub.Application.Interfaces
{
    public interface IReviewVoteService
    {
        Task VoteAsync(Guid reviewId, Guid userId, bool isUpvote);
    }
}
