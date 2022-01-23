using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Repositories
{
    /// <summary>
    /// Defines functionality for a review repository.
    /// </summary>
    public interface IReviewRepository
    {
        List<Review> GetAllByProduct(Product product);
        Review GetReviewById(int id);
        void AddReview(Review review);
        void RemoveReview(Review review);
    }
}