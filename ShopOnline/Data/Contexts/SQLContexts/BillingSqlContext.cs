using Exceptions.Billing;
using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Contexts
{
    public class BillingSqlContext : IBillingContext
    {
        private MySqlConnection connection;

        public Billing GetBillingById(int id)
        {
        try
            {
                Billing billing = new Billing();
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Billing WHERE ID=@BillingID", connection))
                    {
                        command.Parameters.AddWithValue("@BillingID", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                billing.ID = (int)reader["ID"];
                                billing.FirstName = (string)reader["FirstName"];
                                billing.LastName = (string)reader["LastName"];
                                billing.PhoneNumber = (string)reader["Phone"];
                                billing.Address = (string)reader["Address"];
                                billing.City = (string)reader["City"];
                            }
                            return billing;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetBillingByIdFailedException();
            }
        }

        public void AddBilling(Billing billing)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Billing(FirstName, LastName, Phone, Address, City) VALUES (@FirstName, @LastName, @PhoneNumber, @Address, @City)", connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", billing.FirstName);
                        command.Parameters.AddWithValue("@LastName", billing.LastName);
                        command.Parameters.AddWithValue("@PhoneNumber", billing.PhoneNumber);
                        command.Parameters.AddWithValue("@Address", billing.Address);
                        command.Parameters.AddWithValue("@City", billing.City);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingBillingFailedException();
            }
        }

        public void RemoveBilling(Billing billing)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM Billing WHERE ID=@BillingID", connection))
                    {
                        command.Parameters.AddWithValue("@BillingID", billing.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingBillingFailedException();
            }
        }
    }
}