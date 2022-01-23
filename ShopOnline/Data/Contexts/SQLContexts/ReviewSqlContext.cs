using Exceptions.Review;
using Interfaces.Contexts;
using Interfaces.Logic;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Data.Contexts
{
    public class ReviewSqlContext : IReviewContext
    {
        private MySqlConnection connection;

        public List<Review> GetAllByProduct(Product product)
        {
            List<Review> reviews = new List<Review>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Reviews WHERE ProductID=@ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", product.ID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    ID = (int)reader["UserID"]
                                };
                                Review review = new Review
                                {
                                    ID = (int)reader["ID"],
                                    User = user,
                                    Product = product,
                                    Stars = (int)reader["Stars"],
                                    Message = (string)reader["Review"]
                                };
                                reviews.Add(review);
                            }
                            return reviews;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllReviewsFailedException();
            }
        }

        public Review GetReviewById(int id)
        {
            try
            {
                Review review = new Review();
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Reviews WHERE ID=@ReviewID", connection))
                    {
                        command.Parameters.AddWithValue("@ReviewID", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                User user = new User
                                {
                                    ID = (int)reader["UserID"]
                                };
                                Product product = new Product
                                {
                                    ID = (int)reader["ProductID"]
                                };
                                review.ID = (int)reader["ID"];
                                review.User = user;
                                review.Product = product;
                                review.Stars = (int)reader["Stars"];
                                review.Message = (string)reader["Review"];
                            }
                            return review;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetReviewByIDFailedException();
            }
        }
        public void AddReview(Review review)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Reviews(UserID, ProductID, Stars, Review) VALUES (@UserID,@ProductID,@Stars,@Message)", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", review.User.ID);
                        command.Parameters.AddWithValue("@ProductID", review.Product.ID);
                        command.Parameters.AddWithValue("@Stars", review.Stars);
                        command.Parameters.AddWithValue("@Message", review.Message);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingReviewFailedException();
            }
        }

        public void RemoveReview(Review review)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM Reviews WHERE ID=@ReviewID", connection))
                    {
                        command.Parameters.AddWithValue("@ReviewID", review.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingReviewFailedException();
            }
        }
    }
}