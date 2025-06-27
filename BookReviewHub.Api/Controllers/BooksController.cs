using BookReviewHub.Application.Interfaces;
using BookReviewHub.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    // GET /api/books?genre=Fantasy&year=2022
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> Get([FromQuery] string? genre, [FromQuery] int? year)
    {
        var books = await _bookService.GetFilteredAsync(genre, year);
        return Ok(books);
    }

    // GET /api/books/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BookDto>> GetById(Guid id)
    {
        var book = await _bookService.GetByIdAsync(id);
        return book is null ? NotFound() : Ok(book);
    }

    // POST /api/books
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBookDto dto)
    {
        var id = await _bookService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // GET /api/books/{id}/reviews
    [HttpGet("{id:guid}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviewsForBook(Guid id)
    {
        var reviews = await _bookService.GetReviewsForBookAsync(id);
        return Ok(reviews);
    }
}
