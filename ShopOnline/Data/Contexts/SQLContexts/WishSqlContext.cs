using Exceptions.Wishes;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Data.Contexts
{
    public class WishSqlContext : IWishContext
    {
        private MySqlConnection connection;

        public void AddToWishList(Product product, User user)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Wishes VALUES (@UserID, @ProductID)", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", user.ID);
                        command.Parameters.AddWithValue("@ProductID", product.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingWishFailedException();
            }
        }


        public void RemoveFromWishList(Product product, User user)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM Wishes WHERE UserID=@UserID AND ProductID=@ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", user.ID);
                        command.Parameters.AddWithValue("@ProductID", product.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingWishesFailedException();
            }
        }

        public List<Product> GetWishesByUser(User user)
        {
            List<Product> wishes = new List<Product>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products INNER JOIN Wishes ON Wishes.ProductID = Products.ID WHERE Wishes.UserID=@UserID ORDER BY Price DESC", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", user.ID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ID = (int)reader["ID"],
                                    Name = (string)reader["Name"],
                                    Description = (string)reader["Description"],
                                    Price = (double)reader["Price"],
                                    ImageURL = (string)reader["ImageUrl"]
                                };
                                wishes.Add(product);
                            }
                            return wishes;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllWishesFailedException();
            }
        }
    }
}