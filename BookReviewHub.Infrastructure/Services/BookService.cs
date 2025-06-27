using BookReviewHub.Application.Interfaces;
using BookReviewHub.Application.Models;
using BookReviewHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookReviewHub.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _db;

    public BookService(ApplicationDbContext db) => _db = db;

    public async Task<Guid> CreateAsync(CreateBookDto dto)
    {
        var book = new Domain.Entities.Book(dto.Title, dto.Author, dto.PublishedYear, dto.Genre);
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return book.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _db.Books.FindAsync(id) ?? throw new KeyNotFoundException();
        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookDto>> GetAllAsync()
        => await _db.Books
            .AsNoTracking()
            .Select(b => new BookDto(b.Id, b.Title, b.Author, b.PublishedYear, b.Genre))
            .ToListAsync();

    public async Task<BookDto?> GetByIdAsync(Guid id)
    {
        var b = await _db.Books.FindAsync(id);
        return b is null
            ? null
            : new BookDto(b.Id, b.Title, b.Author, b.PublishedYear, b.Genre);
    }

    public async Task UpdateAsync(UpdateBookDto dto)
    {
        var book = await _db.Books.FindAsync(dto.Id) ?? throw new KeyNotFoundException();
        // π.χ. internal Update method ή properties:
        _db.Books.Update(book);
        await _db.SaveChangesAsync();
    }
}
