using Interfaces.Contexts;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Data.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IReviewContext Context;

        public ReviewRepository(IReviewContext context)
        {
            Context = context;
        }
        public List<Review>GetAllByProduct(Product product)
        {
            return Context.GetAllByProduct(product);
        }

        public Review GetReviewById(int id)
        {
            return Context.GetReviewById(id);
        }

        public void AddReview(Review review)
        {
            Context.AddReview(review);
        }

        public void RemoveReview(Review review)
        {
            Context.RemoveReview(review);
        }
    }
}