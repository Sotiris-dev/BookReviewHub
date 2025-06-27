using BookReviewHub.Application.Models;

namespace BookReviewHub.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(Guid id);
        Task<Guid> CreateAsync(CreateBookDto dto);
        Task UpdateAsync(UpdateBookDto dto);
        Task DeleteAsync(Guid id);
    }
}
