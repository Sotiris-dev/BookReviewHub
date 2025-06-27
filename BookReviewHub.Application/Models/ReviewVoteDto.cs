namespace BookReviewHub.Application.Models;

public record ReviewVoteDto(
    Guid Id,
    Guid ReviewId,
    Guid UserId,
    bool IsUpvote
);
