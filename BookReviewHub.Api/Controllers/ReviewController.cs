using BookReviewHub.Application.Interfaces;
using BookReviewHub.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> Get()
    {
        var reviews = await _reviewService.GetAllAsync();
        return Ok(reviews);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReviewDto>> GetById(Guid id)
    {
        var review = await _reviewService.GetByIdAsync(id);
        return review is null ? NotFound() : Ok(review);
    }

    // ▶ POST /api/reviews
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateReviewDto dto)
    {
        var id = await _reviewService.AddReviewAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // ▶ GET /api/reviews/book/{bookId}
    [HttpGet("book/{bookId:guid}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetByBook(Guid bookId)
    {
        var reviews = await _reviewService.GetReviewsForBookAsync(bookId);
        return Ok(reviews);
    }
}
