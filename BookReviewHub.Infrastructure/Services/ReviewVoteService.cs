using BookReviewHub.Application.Interfaces;
using BookReviewHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookReviewHub.Infrastructure.Services;

public class ReviewVoteService : IReviewVoteService
{
    private readonly ApplicationDbContext _db;

    public ReviewVoteService(ApplicationDbContext db) => _db = db;

    public async Task VoteAsync(Guid reviewId, Guid userId, bool isUpvote)
    {
        var existing = await _db.ReviewVotes
            .FirstOrDefaultAsync(rv => rv.ReviewId == reviewId && rv.UserId == userId);

        if (existing is not null)
        {
            existing = new Domain.Entities.ReviewVote(reviewId, userId, isUpvote);
            _db.ReviewVotes.Update(existing);
        }
        else
        {
            var vote = new Domain.Entities.ReviewVote(reviewId, userId, isUpvote);
            _db.ReviewVotes.Add(vote);
        }

        await _db.SaveChangesAsync();
    }
}
