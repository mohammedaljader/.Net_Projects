using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DataAccess
{
    public class MessageDAL : IMessageDAL
    {
        public void SendMessage(MessageDto message)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("insert into message(Message_ID, Send_Date,Subject,Message, JobSeeker_ID,Publisher_ID, Job_ID  ) values(DEFAULT, DEFAULT,@Subject, @Message,@JobSeeker_ID,@Publisher_ID, @Job_ID )", conn))
                    {
                        cmd.Parameters.AddWithValue("@Subject", message.Message_subject );
                        cmd.Parameters.AddWithValue("@Message", message.Message_Text);
                        cmd.Parameters.AddWithValue("@Publisher_ID", message.Publisher_Id.Company_Id);
                        cmd.Parameters.AddWithValue("@JobSeeker_ID", message.JobSeeker_Id.Seeker_Id);
                        cmd.Parameters.AddWithValue("@Job_ID", message.Job_Id.Job_Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex) { Console.WriteLine(ex.Message); }
        }
        public void DeleteMessage(MessageDto message)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Delete from message where Message_ID=@Message_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Message_ID", message.Message_Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (MySqlException ex) { Console.WriteLine(ex.Message); }
        }
        public void EditMessage(MessageDto message)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("update message set Subject=@Subject,Message = @Message where Message_ID=@Message_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Subject", message.Message_subject);
                        cmd.Parameters.AddWithValue("@Message", message.Message_Text);
                        cmd.Parameters.AddWithValue("@Message_ID", message.Message_Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex) { Console.WriteLine(ex.Message); }
        }
        public List<MessageDto> GetAllMessages(int id)
        {
            try
            {
                List<MessageDto> messages = new List<MessageDto>();

                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from message where Job_ID = @Job_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Job_ID", id);
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            MessageDto message = new MessageDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5));
                            messages.Add(message);
                        }
                    }
                }
                return messages;
            }
            catch (MySqlException ex) { Console.WriteLine(ex.Message); return null; }
        }
    }
}
