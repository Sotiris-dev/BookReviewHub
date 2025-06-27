using BookReviewHub.Application.Models;

namespace BookReviewHub.Application.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewsForBookAsync(Guid bookId);
        Task<Guid> AddReviewAsync(CreateReviewDto dto);
    }
}
