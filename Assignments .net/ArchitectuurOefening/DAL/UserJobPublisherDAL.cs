using DAL_interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserJobPublisherDAL : IUser_companyDAL
    {
        public void Register(CompanyDto company)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand RegisterUser = new MySqlCommand("INSERT INTO user(Username, Password, FullName, Email,Telephone, Address) VALUES (@Username, @Password, @FullName, @Email, @Telephone, @Address)", conn))
                    {
                        conn.Open();
                        RegisterUser.Parameters.AddWithValue("@Username", company.Username);
                        RegisterUser.Parameters.AddWithValue("@Password", company.Password);
                        RegisterUser.Parameters.AddWithValue("@FullName", company.Fullname);
                        RegisterUser.Parameters.AddWithValue("@Email", company.Email);
                        RegisterUser.Parameters.AddWithValue("@Telephone", company.Telephone);
                        RegisterUser.Parameters.AddWithValue("@Address", company.Address);
                        RegisterUser.ExecuteNonQuery();
                    }
                }
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand RegisterJobSeeker = new MySqlCommand("INSERT INTO jobpublisher(Publisher_ID, User_ID, Company_Name, Company_address) VALUES(DEFAULT,LAST_INSERT_ID(), @Company_Name, @Company_address  )", conn))
                    {
                        conn.Open();
                        RegisterJobSeeker.Parameters.AddWithValue("@Company_Name", company.Company_Name);
                        RegisterJobSeeker.Parameters.AddWithValue("@Company_address", company.Company_Address);
                        RegisterJobSeeker.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex) { Console.WriteLine(ex.Message); }
        }
        public CompanyDto Login(string username, string password)
        {
            CompanyDto JobPublisher = null;
            int UserID = 0;
            string fullname = "", email = "", telephone = "", address = "";
            try
            {
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
                                fullname = ReadData.GetString(1);
                                username = ReadData.GetString(2);
                                password = ReadData.GetString(3);
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
                    using (MySqlCommand Login = new MySqlCommand("SELECT * from jobpublisher WHERE User_ID  = @User_ID", conn))
                    {
                        conn.Open();
                        Login.Parameters.AddWithValue("@User_ID", UserID);
                        using (MySqlDataReader Reader = Login.ExecuteReader())
                        {
                            while (Reader.Read())
                            {
                                JobPublisher = new CompanyDto(UserID, fullname, username, password, email, telephone, address, Reader.GetInt32(0), Reader.GetString(1), Reader.GetString(2));
                            }
                            return JobPublisher;
                        }
                    }
                }
               // return JobPublisher;
            }
            catch (MySqlException ex) { Console.WriteLine(ex.Message); return JobPublisher; }
        }
        public void EditProfile(CompanyDto company)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("update user set Username=@Username,Password=@Password,FullName = @FullName, Email = @Email, Telephone = @Telephone ,Address= @Address    where User_ID=@User_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", company.Username);
                        cmd.Parameters.AddWithValue("@Password", company.Password);
                        cmd.Parameters.AddWithValue("@FullName", company.Fullname);
                        cmd.Parameters.AddWithValue("@Email", company.Email);
                        cmd.Parameters.AddWithValue("@Telephone", company.Telephone);
                        cmd.Parameters.AddWithValue("@Address", company.Address);
                        cmd.Parameters.AddWithValue("@User_ID", company.Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    using (MySqlCommand cmd = new MySqlCommand("update jobpublisher set Company_Name=@Experience,Hobby=@Hobby where User_ID=@User_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Company_Name", company.Company_Name);
                        cmd.Parameters.AddWithValue("@Company_address", company.Company_Address);
                        cmd.Parameters.AddWithValue("@User_ID", company.Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex) { Console.WriteLine(ex.Message); }
        }

        public List<CompanyDto> GetAllJobPublishers()
        {
            List<CompanyDto> companyDtos = new List<CompanyDto>();
            string Query = @"SELECT u.User_ID,u.FullName, u.Username , u.Password , u.Email , u.Telephone , u.Address , jp.Publisher_ID , jp.Company_Name , jp.Company_address FROM user u INNER join jobpublisher jp on u.User_ID = jp.User_ID";
            using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(Query, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CompanyDto company = new CompanyDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetInt32(7), reader.GetString(8), reader.GetString(9));
                        companyDtos.Add(company);
                    }
                    return companyDtos;
                }
            }
        }
    }
}
