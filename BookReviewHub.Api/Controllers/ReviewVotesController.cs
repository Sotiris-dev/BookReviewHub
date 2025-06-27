using BookReviewHub.Application.Interfaces;
using BookReviewHub.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewVotesController : ControllerBase
{
    private readonly IReviewVoteService _reviewVoteService;

    public ReviewVotesController(IReviewVoteService reviewVoteService)
    {
        _reviewVoteService = reviewVoteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewVoteDto>>> Get()
    {
        var votes = await _reviewVoteService.GetAllAsync();
        return Ok(votes);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReviewVoteDto>> GetById(Guid id)
    {
        var vote = await _reviewVoteService.GetByIdAsync(id);
        return vote is null ? NotFound() : Ok(vote);
    }

    [HttpPost("reviews/{id:guid}/vote")]
    public async Task<IActionResult> Vote(Guid id, [FromBody] VoteRequestDto dto)
    {
        await _reviewVoteService.VoteAsync(id, dto.UserId, dto.IsUpvote);
        return Ok();
    }

}
