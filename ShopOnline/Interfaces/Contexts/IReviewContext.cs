using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a review context.
    /// </summary>
    public interface IReviewContext
    {
        List<Review> GetAllByProduct(Product product);
        Review GetReviewById(int id);
        void AddReview(Review review);
        void RemoveReview(Review review);
    }
}