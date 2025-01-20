using Entities;

namespace Services
{
    public interface IRatingService
    {
        Task<Rating> addRating(Rating rating);
    }
}