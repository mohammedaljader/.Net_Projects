using Exceptions.Order;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Data.Contexts
{
    public class OrderSqlContext : IOrderContext
    {
        private MySqlConnection connection;
        public List<Order> GetAllOrdersByUser(User user)
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Orders WHERE UserID=@UserID ", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", user.ID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<Product> products = new List<Product>();
                                Order order = new Order();
                                order.ID = (int)reader["ID"];
                                order.User = user;
                                order.Status = (string)reader["Status"];
                                order.Date = reader["Date"] == DBNull.Value ? DateTime.Now : (DateTime)reader["Date"];

                                using (MySqlConnection getProductsConnection = DataConnection.getConnection())
                                {
                                    getProductsConnection.Open();
                                    using (MySqlCommand getProducts = new MySqlCommand("SELECT * FROM Products INNER JOIN OrderProducts ON OrderProducts.ProductID = Products.ID WHERE OrderProducts.OrderID=@OrderID", getProductsConnection))
                                    {
                                        getProducts.Parameters.AddWithValue("@OrderID", order.ID);
                                        using (MySqlDataReader getProductsReader = getProducts.ExecuteReader())
                                        {
                                            while (getProductsReader.Read())
                                            {
                                                Product product = new Product
                                                {
                                                    ID = (int)getProductsReader["ID"],
                                                    Name = (string)getProductsReader["Name"],
                                                    Description = (string)getProductsReader["Description"],
                                                    Price = (double)getProductsReader["Price"],
                                                    ImageURL = (string)getProductsReader["ImageUrl"]
                                                };
                                                products.Add(product);
                                            }
                                        }
                                    }
                                }
                                using (MySqlConnection getBillingConnection = DataConnection.getConnection())
                                {
                                    getBillingConnection.Open();
                                    Billing billing = new Billing();
                                    using (MySqlCommand getBilling = new MySqlCommand("SELECT * FROM Billing WHERE ID=@BillingID", getBillingConnection))
                                    {
                                        getBilling.Parameters.AddWithValue("@BillingID", reader["BillingID"]);
                                        using (MySqlDataReader getBillingReader = getBilling.ExecuteReader())
                                        {
                                            while (getBillingReader.Read())
                                            {
                                                billing.ID = (int)getBillingReader["ID"];
                                                billing.FirstName = (string)getBillingReader["FirstName"];
                                                billing.LastName = (string)getBillingReader["LastName"];
                                                billing.PhoneNumber = (string)getBillingReader["Phone"];
                                                billing.Address = (string)getBillingReader["Address"];
                                                billing.City = (string)getBillingReader["City"];
                                            }
                                        }
                                    }

                                    order.Billing = billing;
                                }
                                order.Products = products;
                                orders.Add(order);
                            }
                            return orders;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllOrdersByUserFailedException();
            }
        }

        public void AddOrder(Order order)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Orders(UserID, BillingID, Status, Date) VALUES " +
                        "(@UserID, (SELECT ID FROM Billing ORDER BY ID DESC LIMIT 1), @Status, @Date)", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", order.User.ID);
                        command.Parameters.AddWithValue("@Status", order.Status);
                        command.Parameters.AddWithValue("@Date", order.Date);
                        command.ExecuteNonQuery();
                    }
                    foreach (Product product in order.Products)
                    {
                        using (MySqlCommand command = new MySqlCommand("INSERT INTO OrderProducts(ProductID , OrderID, Price) VALUES (@ProductID, (SELECT ID FROM Orders ORDER BY ID DESC LIMIT 1), @Price) ", connection))
                        {
                            command.Parameters.AddWithValue("@ProductID", product.ID);
                            command.Parameters.AddWithValue("@Price", product.Price);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingOrderFailedException();
            }
        }

        public void DeleteOrder(Order order)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE OrderProducts.*, Orders.* FROM OrderProducts " +
                        "LEFT JOIN Orders ON Orders.ID = OrderProducts.OrderID WHERE OrderProducts.OrderID=@OrderID", connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", order.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingOrderFailedException();
            }
        }
    }
}