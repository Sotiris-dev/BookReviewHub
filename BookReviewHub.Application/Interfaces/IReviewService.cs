using BookReviewHub.Application.Models;

namespace BookReviewHub.Application.Interfaces;

public interface IReviewService
{
    Task<Guid> AddReviewAsync(CreateReviewDto dto);
    Task<IEnumerable<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(Guid id);
    Task<IEnumerable<ReviewDto>> GetReviewsForBookAsync(Guid bookId);
}
