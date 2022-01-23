using DAL_interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BookDAL : IBookDAL
    {
        private string connectionString = "Server = localhost; Uid=root;Database=oefening;";

        public void CreateBook(BookDto book)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into gegevens(Name, Age, Telefoon,Email ) values(@name , @age, @telefoon, @email)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", book.Name);
                    cmd.Parameters.AddWithValue("@age", book.Age);
                    cmd.Parameters.AddWithValue("@telefoon", book.Telephone);
                    cmd.Parameters.AddWithValue("@email", book.Email);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public BookDto FindBookByID(int id)
        {
            //   // BookDto book = new BookDto();
            //    using (MySqlConnection conn = new MySqlConnection(connectionString))
            //    {
            //        using (MySqlCommand cmd = new MySqlCommand("select * from gegevens where idgegevens = @idgegevens", conn))
            //        {
            //            cmd.Parameters.AddWithValue("@idgegevens", id);
            //            conn.Open();
            //            MySqlDataReader reader = cmd.ExecuteReader();
            //            while (reader.Read())
            //            {
            //                book.Id = reader.GetInt32(0);
            //                book.Name = reader.GetString(1);
            //                book.Age = reader.GetInt32(2);
            //                book.Telephone = reader.GetString(3);
            //                book.Email = reader.GetString(4);
            //            }
            //        }

            // }
            return null;
        }

        public List<BookDto> GetAllBooks()
        {
            List<BookDto> books = new List<BookDto>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from gegevens", conn))
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BookDto book = new BookDto(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                        //book.Id = reader.GetInt32(0);
                        //book.Name = reader.GetString(1);
                        //book.Age = reader.GetInt32(2);
                        //book.Telephone = reader.GetString(3);
                        //book.Email = reader.GetString(4);
                        books.Add(book);
                    }
                }
            }
            return books;
        }

        public void RemoveBook(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("Delete from gegevens where idgegevens=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void UpdateBook(BookDto book)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("update gegevens set Name=@name,Age=@age,Telefoon=@telefoon,Email=@email  where idgegevens=@id", conn))
                {

                    cmd.Parameters.AddWithValue("@name", book.Name);
                    cmd.Parameters.AddWithValue("@age", book.Age);
                    cmd.Parameters.AddWithValue("@telefoon", book.Telephone);
                    cmd.Parameters.AddWithValue("@email", book.Email);
                    cmd.Parameters.AddWithValue("@id", book.Id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
    }
}
