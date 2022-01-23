using Data;
using Data.Contexts;
using Data.Repositories;
using Exceptions.Review;
using Interfaces.Contexts;
using Interfaces.Logic;
using Interfaces.Repositories;
using Models.DataModels;
using System.Collections.Generic;

namespace Logic.LogicObjects
{
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IUserLogic userLogic = new UserLogic(new UserSqlContext());
        private readonly IProductLogic productLogic = new ProductLogic(new ProductSqlContext());

        public ReviewLogic(IReviewContext context)
        {
            reviewRepository = new ReviewRepository(context);
        }

        public List<Review>GetAllByProduct(Product product)
        {
            List<Review> reviews = reviewRepository.GetAllByProduct(product);
            foreach (Review review in reviews)
            {
                review.User = userLogic.GetUserById(review.User.ID);
                review.Product = productLogic.GetProductById(review.Product.ID);
            }
            return reviews;
        }

        public Review GetReviewById(int id)
        {
            return reviewRepository.GetReviewById(id);
        }

        public void AddReview(Review review)
        {
            if (review.Message.Length > 50 || review.Stars > 5)
            {
                throw new AddingReviewFailedException();
            }
            reviewRepository.AddReview(review);
        }
        public void RemoveReview(Review review)
        {
            reviewRepository.RemoveReview(review);
        }
    }
}