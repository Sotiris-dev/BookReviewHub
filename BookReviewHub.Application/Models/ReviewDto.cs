namespace BookReviewHub.Application.Models
{
    public class ReviewDto
    {
        public Guid Id { get; init; }
        public Guid BookId { get; init; }
        public string UserId { get; init; }
        public string Content { get; init; }
        public int Rating { get; init; }
        public DateTime DateCreated { get; init; }

        public ReviewDto(Guid id, Guid bookId, string userId, string content, int rating, DateTime dateCreated)
        {
            Id = id;
            BookId = bookId;
            UserId = userId;
            Content = content;
            Rating = rating;
            DateCreated = dateCreated;
        }
    }


}
