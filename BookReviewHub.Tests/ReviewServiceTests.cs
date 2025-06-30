using BookReviewHub.Application.Models;
using BookReviewHub.Infrastructure.Services;

namespace BookReviewHub.Tests
{
    public class ReviewServiceTests
    {
        [Fact]
        public async Task AddReviewStoresReview()
        {
            var ctx = TestDbContextFactory.Create();
            var service = new ReviewService(ctx);
            var dto = new CreateReviewDto
            {
                BookId = Guid.NewGuid(),
                UserId = "user1",
                Content = "Great!",
                Rating = 5
            };

            var id = await service.AddReviewAsync(dto);
            var review = await ctx.Reviews.FindAsync(id);

            Assert.NotNull(review);
            Assert.Equal(dto.Content, review.Content);
            Assert.Equal(dto.Rating, review.Rating);
            Assert.Equal(dto.UserId, review.UserId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        public async Task AddReview_InvalidRating_ThrowsAsync(int invalidRating)
        {
            var ctx = TestDbContextFactory.Create();
            var dto = new CreateReviewDto
            {
                BookId = Guid.NewGuid(),
                UserId = "u",
                Content = "X",
                Rating = invalidRating
            };

            var service = new ReviewService(ctx);
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => service.AddReviewAsync(dto));
        }
    }

}
