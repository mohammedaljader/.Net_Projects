using Exceptions.Product;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Data.Contexts
{
    public class ProductSqlContext : IProductContext
    {
        private MySqlConnection connection;

        public void AddProduct(Product product)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Products (`Name`, `Description`, `Price`, `ImageUrl`) VALUES (@Name, @Description, @Price, @ImageUrl)", connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@ImageUrl", product.ImageURL);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingProductFailedException();
            }
        }


        public void RemoveProduct(Product product)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM Products WHERE ID=@ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", product.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingProductFailedException();
            }
        }

        public void EditProduct(Product product)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("UPDATE Products SET ID=@ProductID,ImageURL=@ImageUrl,Name=@Name,Description=@Description,Price=@Price WHERE ID=@ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", product.ID);
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@ImageUrl", product.ImageURL);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new UpdatingProductFailedException();
            }
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products ORDER BY Price DESC", connection))
                    {
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
                                products.Add(product);
                            }
                            return products;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllProductsFailedException();
            }
        }

        public Product GetProductById(int id)
        {
            try
            {
                Product product = new Product();
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products WHERE ID=@ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                product.ID = (int)reader["ID"];
                                product.Name = (string)reader["Name"];
                                product.Description = (string)reader["Description"];
                                product.Price = (double)reader["Price"];
                                product.ImageURL = (string)reader["ImageUrl"];       
                            }
                            return product;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetProductByIdFailedException();
            }
        }
    }
}