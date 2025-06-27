using BookReviewHub.Application.Interfaces;
using BookReviewHub.Application.Models;
using BookReviewHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookReviewHub.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;

    public UserService(ApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return await _db.Users
            .AsNoTracking()
            .Select(u => new UserDto(
                Guid.Parse(u.Id),
                u.UserName!,
                u.Email!))
            .ToListAsync();
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var u = await _db.Users.FindAsync(id.ToString());
        return u is null
            ? null
            : new UserDto(
                Guid.Parse(u.Id),
                u.UserName!,
                u.Email!);
    }
}
