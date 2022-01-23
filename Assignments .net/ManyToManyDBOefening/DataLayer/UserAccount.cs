using DataModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserAccount
    {
        string connection = @"Server=studmysql01.fhict.local;Uid=dbi461166;Database=dbi461166;Pwd=fontys;";


        public void Register(User user, JobSeeker jobSeeker)
        {

            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                using (MySqlCommand RegisterUser = new MySqlCommand("INSERT INTO user(Username, Password, FullName, Email,Telephone, Address) VALUES (@Username, @Password, @FullName, @Email, @Telephone, @Address)", conn))
                {
                    conn.Open();
                    RegisterUser.Parameters.AddWithValue("@Username", user.Username);
                    RegisterUser.Parameters.AddWithValue("@Password", user.Password);
                    RegisterUser.Parameters.AddWithValue("@FullName", user.FullName);
                    RegisterUser.Parameters.AddWithValue("@Email", user.Email);
                    RegisterUser.Parameters.AddWithValue("@Telephone", user.Telephone);
                    RegisterUser.Parameters.AddWithValue("@Address", user.Address);
                    RegisterUser.ExecuteNonQuery();   
                }
                using (MySqlCommand RegisterJobSeeker = new MySqlCommand("INSERT INTO jobseeker(JobSeeker_ID, User_ID, Experience, Hobby) VALUES(DEFAULT,LAST_INSERT_ID(), @Experience, @Hobby  )", conn))
                {
                    RegisterJobSeeker.Parameters.AddWithValue("@Experience", jobSeeker.Experience);
                    RegisterJobSeeker.Parameters.AddWithValue("@Hobby", jobSeeker.Hobby);

                    RegisterJobSeeker.ExecuteNonQuery();
                }

            }
        }
    }
}
