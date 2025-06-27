using BookReviewHub.Application.Models;

namespace BookReviewHub.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllAsync();
    }
}
