using DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class UserDAL : IUserDAL
    {
        private string connectionString = "Server = localhost; Uid=root;Database=oefening;";

        public void AddUser(UserDto userDto)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("insert into user(Name, Age, Email ) values(@name , @age, @email)", conn))
                {
                    cmd.Parameters.AddWithValue("@name", userDto.Name);
                    cmd.Parameters.AddWithValue("@age", userDto.Age);
                    cmd.Parameters.AddWithValue("@email", userDto.Email);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public void EditUser(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetAllUsers()
        {
            List<UserDto> users = new List<UserDto>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("select * from user", conn))
                {
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserDto userDto = new UserDto(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3));
                        users.Add(userDto);
                    }
                }
            }
            return users;
        }
    }
}
