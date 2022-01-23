

using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Logic
{
    /// <summary>
    /// Defines functionality for a review logic class.
    /// </summary>
    public interface IReviewLogic
    {
        List<Review> GetAllByProduct(Product product);
        Review GetReviewById(int id);
        void AddReview(Review review);
        void RemoveReview(Review review);
    }
}