using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions.Categorie;

namespace DAL.DataAccess
{
    public class CategorieDAL : ICategorieDAL
    {
        public void AddCategorie(CategorieDto categorieDto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("insert into categorie(Publisher_ID, Categorie_name,Categorie_description) values(@Publisher_ID, @Categorie_name, @Categorie_description)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Publisher_ID", categorieDto.JobPublisher_id);
                        cmd.Parameters.AddWithValue("@Categorie_name", categorieDto.Categorie_name);
                        cmd.Parameters.AddWithValue("@Categorie_description", categorieDto.Categorie_description);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingCategorieFailedException("Unable to add new categorie, try again please");
            }
        }

        public void DeleteCategorie(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Delete from categorie where Categorie_ID=@Categorie_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Categorie_ID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingCategorieFailedException("Unable to delete this categorie, try again please");
            }
        }

        public void EditCategorie(CategorieDto categorieDto)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("update categorie set Publisher_ID=@Publisher_ID,Categorie_name=@Categorie_name,Categorie_description=@Categorie_description  where Categorie_ID=@Categorie_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Publisher_ID", categorieDto.JobPublisher_id);
                        cmd.Parameters.AddWithValue("@Categorie_name", categorieDto.Categorie_name);
                        cmd.Parameters.AddWithValue("@Categorie_description", categorieDto.Categorie_description);
                        cmd.Parameters.AddWithValue("@Categorie_ID", categorieDto.Categorie_Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new UpdatingCategorieFailedException("Unable to update this categorie, try again please");
            }
        }

        public CategorieDto FindCategorie(int id)
        {
            try
            {
                CategorieDto categorieDto = null;
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from categorie where Categorie_ID = @Categorie_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Categorie_ID", id);
                        conn.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            categorieDto = new CategorieDto(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3)); 
                        }
                        return categorieDto;
                    }
                }
            }
            catch (MySqlException)
            {
                throw new FindingCategorieFailedException("Unable to find the categorie, please try again");
            }
        }

        //public CategorieDto FindCategorieByID(int id)
        //{
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
        //        {
        //            using (MySqlCommand cmd = new MySqlCommand("select * from categorie where Categorie_ID = @Categorie_ID", conn))
        //            {
        //                cmd.Parameters.AddWithValue("@Categorie_ID", id);
        //                conn.Open();
        //                MySqlDataReader reader = cmd.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    CategorieDto categorie = new CategorieDto(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
        //                    return categorie;
        //                }
        //            }
        //        }
        //        return null;
        //    }
        //    catch (MySqlException ex) { Console.WriteLine(ex.Message);  return null; }
        //}

        public List<CategorieDto> GetAllCategories()
        {
            try
            {
                List<CategorieDto> categories = new List<CategorieDto>();
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from categorie", conn))
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CategorieDto categorieDto = new CategorieDto(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3));
                            categories.Add(categorieDto);
                        }
                    }
                }
                return categories;
            }
            catch (MySqlException)
            {
                throw new GetAllCategoriesFailedException("Unable to get all categories, please try again");
            }
        }

        public List<CategorieDto> GetCategoriesByJobPublisherId(int id)
        {
            try
            {
                List<CategorieDto> categories = new List<CategorieDto>();
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from categorie WHERE categorie.Publisher_ID = @publisherid ", conn))
                    {
                        cmd.Parameters.AddWithValue("@publisherid", id);
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            CategorieDto categorieDto = new CategorieDto();
                            categorieDto.Categorie_Id = reader.GetInt32(0);
                           // categorieDto.Publisher_ID.Company_Id = reader.GetInt32(1);
                            categorieDto.Categorie_name = reader.GetString(2);
                            categorieDto.Categorie_description = reader.GetString(3);
                            categories.Add(categorieDto);
                        }
                        return categories;
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllCategoriesByJobPublisherFailedException("Unable to find categories of this user, please try again");
            }
        }
    }
}
