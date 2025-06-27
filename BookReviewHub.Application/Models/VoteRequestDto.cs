namespace BookReviewHub.Application.Models;

public record VoteRequestDto(Guid UserId, bool IsUpvote);
