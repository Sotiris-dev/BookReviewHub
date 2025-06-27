using BookReviewHub.Application.Models;

namespace BookReviewHub.Application.Interfaces;

public interface IReviewVoteService
{
    Task<IEnumerable<ReviewVoteDto>> GetAllAsync();
    Task<ReviewVoteDto?> GetByIdAsync(Guid id);
    Task VoteAsync(Guid reviewId, Guid userId, bool isUpvote);

}
