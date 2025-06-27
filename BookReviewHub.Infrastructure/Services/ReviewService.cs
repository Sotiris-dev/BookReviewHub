using BookReviewHub.Application.Interfaces;
using BookReviewHub.Application.Models;
using BookReviewHub.Domain.Entities;
using BookReviewHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookReviewHub.Infrastructure.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _db;

    public ReviewService(ApplicationDbContext db) => _db = db;

    public async Task<Guid> AddReviewAsync(CreateReviewDto dto)
    {
        var review = new Review(dto.BookId, dto.UserId, dto.Content, dto.Rating);
        _db.Reviews.Add(review);
        await _db.SaveChangesAsync();
        return review.Id;
    }

    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        return await _db.Reviews
            .AsNoTracking()
            .Select(r => new ReviewDto(r.Id, r.BookId, r.UserId, r.Content, r.Rating, r.DateCreated))
            .ToListAsync();
    }

    public async Task<ReviewDto?> GetByIdAsync(Guid id)
    {
        return await _db.Reviews
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Select(r => new ReviewDto(r.Id, r.BookId, r.UserId, r.Content, r.Rating, r.DateCreated))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsForBookAsync(Guid bookId)
    {
        return await _db.Reviews
            .AsNoTracking()
            .Where(r => r.BookId == bookId)
            .Select(r => new ReviewDto(r.Id, r.BookId, r.UserId, r.Content, r.Rating, r.DateCreated))
            .ToListAsync();
    }
}
