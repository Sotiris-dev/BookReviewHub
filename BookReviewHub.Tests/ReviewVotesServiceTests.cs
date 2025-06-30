using BookReviewHub.Infrastructure.Data;
using BookReviewHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BookReviewHub.Tests
{
    public class ReviewVotesServiceTests
    {
        private static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var ctx = new ApplicationDbContext(options);
            ctx.Database.EnsureCreated();
            return ctx;
        }

        [Fact]
        public async Task VoteAsync_CreatesVote()
        {
            var ctx = CreateContext();
            var review = new Review(Guid.NewGuid(), "u1", "R", 4);
            ctx.Reviews.Add(review);
            await ctx.SaveChangesAsync();

            var service = new ReviewVoteService(ctx);
            await service.VoteAsync(review.Id, Guid.Parse("00000000-0000-0000-0000-000000000002"), isUpvote: true);

            var vote = await ctx.ReviewVotes.SingleOrDefaultAsync(v =>
                v.ReviewId == review.Id &&
                v.UserId == Guid.Parse("00000000-0000-0000-0000-000000000002"));
            Assert.NotNull(vote);
            Assert.True(vote.IsUpvote);
        }

        [Fact]
        public async Task VoteAsync_SameUserTwice_UpdatesVote()
        {
            var ctx = CreateContext();
            var review = new Review(Guid.NewGuid(), "u1", "R", 4);
            ctx.Reviews.Add(review);
            await ctx.SaveChangesAsync();

            var userId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            var service = new ReviewVoteService(ctx);

            // First: upvote
            await service.VoteAsync(review.Id, userId, isUpvote: true);
            var vote1 = await ctx.ReviewVotes.SingleAsync(v =>
                v.ReviewId == review.Id && v.UserId == userId);
            Assert.True(vote1.IsUpvote);

            // Second: change to downvote
            await service.VoteAsync(review.Id, userId, isUpvote: false);
            var vote2 = await ctx.ReviewVotes.SingleAsync(v =>
                v.ReviewId == review.Id && v.UserId == userId);
            Assert.False(vote2.IsUpvote);
            Assert.Equal(vote1.Id, vote2.Id);
        }
    }
}
