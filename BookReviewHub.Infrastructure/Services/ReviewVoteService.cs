using BookReviewHub.Application.Interfaces;
using BookReviewHub.Application.Models;
using BookReviewHub.Domain.Entities;
using BookReviewHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookReviewHub.Infrastructure.Services;

public class ReviewVoteService : IReviewVoteService
{
    private readonly ApplicationDbContext _db;

    public ReviewVoteService(ApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<ReviewVoteDto>> GetAllAsync()
    {
        return await _db.ReviewVotes
            .AsNoTracking()
            .Select(v => new ReviewVoteDto(v.Id, v.ReviewId, v.UserId, v.IsUpvote))
            .ToListAsync();
    }

    public async Task<ReviewVoteDto?> GetByIdAsync(Guid id)
    {
        var v = await _db.ReviewVotes.FindAsync(id);
        return v is null ? null : new ReviewVoteDto(v.Id, v.ReviewId, v.UserId, v.IsUpvote);
    }

    public async Task VoteAsync(Guid reviewId, Guid userId, bool isUpvote)
    {
        var existing = await _db.ReviewVotes
            .FirstOrDefaultAsync(v => v.ReviewId == reviewId && v.UserId == userId);

        if (existing is not null)
        {
            existing.UpdateVote(isUpvote);
        }
        else
        {
            var vote = new ReviewVote(reviewId, userId, isUpvote);
            _db.ReviewVotes.Add(vote);
        }

        await _db.SaveChangesAsync();
    }
}
