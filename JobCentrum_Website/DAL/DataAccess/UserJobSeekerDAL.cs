using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions.JobSeeker;

namespace DAL.DataAccess
{
    public class UserJobSeekerDAL : IUser_JobseekerDAL
    {
        public void EditProfile(Job_SeekerDto job_Seeker)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("update user set Username=@Username,Password=@Password,FullName = @FullName, Email = @Email, Telephone = @Telephone ,Address= @Address    where User_ID=@User_ID", conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@Username", job_Seeker.Username);
                        cmd.Parameters.AddWithValue("@Password", job_Seeker.Password);
                        cmd.Parameters.AddWithValue("@FullName", job_Seeker.Fullname);
                        cmd.Parameters.AddWithValue("@Email", job_Seeker.Email);
                        cmd.Parameters.AddWithValue("@Telephone", job_Seeker.Telephone);
                        cmd.Parameters.AddWithValue("@Address", job_Seeker.Address);
                        cmd.Parameters.AddWithValue("@User_ID", job_Seeker.Id);
                        cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmd = new MySqlCommand("update jobseeker set Experience=@Experience,Hobby=@Hobby where User_ID=@User_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Experience", job_Seeker.Experience);
                        cmd.Parameters.AddWithValue("@Hobby", job_Seeker.Hobby);
                        cmd.Parameters.AddWithValue("@User_ID", job_Seeker.Id); 
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException) 
            {
                throw new UpdatingJobSeekerFailedException("Unable to update account, try again please");
            }
        }
        public List<Job_SeekerDto> GetAllJobSeerkers()
        {
            try
            {
                List<Job_SeekerDto> job_SeekerDtos = new List<Job_SeekerDto>();
                string Query = @"SELECT u.User_ID,u.FullName, u.Username , u.Password , u.Email , u.Telephone , u.Address , js.JobSeeker_ID , js.Experience , js.Hobby FROM user u INNER join jobseeker js on u.User_ID = js.User_ID";
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(Query, conn))
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Job_SeekerDto job_Seeker = new Job_SeekerDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetString(9));
                            job_SeekerDtos.Add(job_Seeker);
                        }
                        return job_SeekerDtos;
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllJobSeekerFailedException("Sorry, Unable to get all users from database, try again please");
            }
            
        }
        public Job_SeekerDto Login(string username, string password)
        {
            try
            {
                Job_SeekerDto job_Seeker = null;
                int UserID = 0;
                string fullname = "", email = "", telephone = "", address = "";

                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    DataTable Table = new DataTable();
                    MySqlDataAdapter DataAdapter = new MySqlDataAdapter();
                    using (MySqlCommand Login = new MySqlCommand("SELECT * FROM user WHERE Username = @username AND Password = @password", conn))
                    {
                        conn.Open();
                        Login.Parameters.AddWithValue("@username", username);
                        Login.Parameters.AddWithValue("@password", password);
                        DataAdapter.SelectCommand = Login;
                        DataAdapter.Fill(Table);
                        if (Table.Rows.Count > 0)
                        {
                            MySqlDataReader ReadData = Login.ExecuteReader();
                            while (ReadData.Read())
                            {
                                UserID = ReadData.GetInt32(0);
                                username = ReadData.GetString(1);
                                password = ReadData.GetString(2);
                                fullname = ReadData.GetString(3);
                                email = ReadData.GetString(4);
                                telephone = ReadData.GetString(5);
                                address = ReadData.GetString(6);
                            }
                        }
                    }
                }
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    DataTable Table = new DataTable();
                    MySqlDataAdapter DataAdapter = new MySqlDataAdapter();
                    using (MySqlCommand Login = new MySqlCommand("SELECT * from jobseeker WHERE User_ID  = @User_ID", conn))
                    {
                        conn.Open();
                        Login.Parameters.AddWithValue("@User_ID", UserID);
                        using (MySqlDataReader Reader = Login.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                job_Seeker = new Job_SeekerDto(UserID, fullname, username, password, email, telephone, address, Reader.GetInt32(0), Reader.GetInt32(2), Reader.GetString(3));
                                return job_Seeker;
                            }
                        }
                    }
                }
                return job_Seeker;
            }
            catch (MySqlException)
            {
                throw new LoginJobSeekerFailedException("Unable to login right now, try again please!");
            }
        }
        public void Register(Job_SeekerDto job_Seeker)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand RegisterUser = new MySqlCommand("INSERT INTO user(Username, Password, FullName, Email,Telephone, Address) VALUES (@Username, @Password, @FullName, @Email, @Telephone, @Address)", conn))
                    {
                        conn.Open();
                        RegisterUser.Parameters.AddWithValue("@Username", job_Seeker.Username);
                        RegisterUser.Parameters.AddWithValue("@Password", job_Seeker.Password);
                        RegisterUser.Parameters.AddWithValue("@FullName", job_Seeker.Fullname);
                        RegisterUser.Parameters.AddWithValue("@Email", job_Seeker.Email);
                        RegisterUser.Parameters.AddWithValue("@Telephone", job_Seeker.Telephone);
                        RegisterUser.Parameters.AddWithValue("@Address", job_Seeker.Address);
                        RegisterUser.ExecuteNonQuery();
                    }
                }
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand RegisterJobSeeker = new MySqlCommand("INSERT INTO jobseeker(JobSeeker_ID, User_ID, Experience, Hobby) VALUES(DEFAULT,LAST_INSERT_ID(), @Experience, @Hobby  )", conn))
                    {
                        conn.Open();
                        RegisterJobSeeker.Parameters.AddWithValue("@Experience", job_Seeker.Experience);
                        RegisterJobSeeker.Parameters.AddWithValue("@Hobby", job_Seeker.Hobby);
                        RegisterJobSeeker.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RegisterJobSeekerFailedException("Unable to register right now, try again please");
            }
        }
    }
}