namespace BookReviewHub.Application.Models
{
    public class CreateReviewDto
    {
        public Guid BookId { get; set; }
        public string UserId { get; set; } = default!;
        public string Content { get; set; } = default!;
        public int Rating { get; set; }
    }

}
