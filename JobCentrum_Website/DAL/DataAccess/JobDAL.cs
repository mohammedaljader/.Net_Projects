using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions.Job;

namespace DAL.DataAccess
{
    public class JobDAL : IJobDAL
    {
        public void AddJob(JobDto job)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("insert into job(Job_title, Job_description,Job_image, Job_location,Categorie_ID,Publisher_ID) values(@Job_title, @Job_description, @Job_image, @Job_location, @Categorie_ID, @Publisher_ID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@Job_title", job.Job_name);
                        cmd.Parameters.AddWithValue("@Job_description", job.Job_description);
                        cmd.Parameters.AddWithValue("@Job_image", job.Job_image);
                        cmd.Parameters.AddWithValue("@Job_location", job.Job_location);
                        cmd.Parameters.AddWithValue("@Categorie_ID", job.Categorie_ID);
                        cmd.Parameters.AddWithValue("@Publisher_ID", job.Publisher_ID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingJobFailedException("Unable to add job to the system, try again please");
            }
        }

        public void DeleteJob(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Delete from job where Job_ID=@Job_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Job_ID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingJobFailedException("Unable to delete this job, please try again");
            }
        }

        public void EditJob(JobDto job)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("update job set Job_title=@Job_title,Job_description=@Job_description,Job_image=@Job_image,Job_location= @Job_location,Categorie_ID=@Categorie_ID, Publisher_ID=@Publisher_ID where Job_ID=@Job_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Job_title", job.Job_name);
                        cmd.Parameters.AddWithValue("@Job_description", job.Job_description);
                        cmd.Parameters.AddWithValue("@Job_image", job.Job_image);
                        cmd.Parameters.AddWithValue("@Job_location", job.Job_location);
                        cmd.Parameters.AddWithValue("@Categorie_ID", job.Categorie_ID);
                        cmd.Parameters.AddWithValue("@Publisher_ID", job.Publisher_ID);
                        cmd.Parameters.AddWithValue("@Job_ID", job.Job_Id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new UpdatingJobFailedException("Unable to edit the job, please try again");
            }
        }

        public JobDto FindJobById(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from job where Job_ID = @Job_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@Job_ID", id);
                        conn.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            JobDto job = new JobDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                            return job;
                        }
                    }
                }
                return null;
            }
            catch (MySqlException)
            {
                throw new FindingJobFailedException("Unable to find the job, please try again");
            }
        }

        public List<JobDto> GetAllJobByJobPublisher(int id)
        { 
            try
            {
                List<JobDto> jobDtos = new List<JobDto>();
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from job WHERE job.Publisher_ID = @publisherid ", conn))
                    {
                        cmd.Parameters.AddWithValue("@publisherid", id);
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            JobDto job = new JobDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                            jobDtos.Add(job);
                        }
                        return jobDtos;
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllJobsByJobPublisherFailedException("Unable to get jobs by job publisher, please try again");
            }
        }

        public List<JobDto> GetAllJobs()
        {
            try
            {
                List<JobDto> Jobs = new List<JobDto>();
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from job", conn))
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            JobDto job = new JobDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                            Jobs.Add(job);
                        }
                        return Jobs;
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllJobsFailedException("Unable to get all jobs, please try again");
            }
        }
        //Categorie_ID
        public List<JobDto> GetAllJobsByCategorieId(int id)
        {
            try
            {
                List<JobDto> jobDtos = new List<JobDto>();
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("select * from job WHERE job.Categorie_ID = @Categorie_ID ", conn))
                    {
                        cmd.Parameters.AddWithValue("@Categorie_ID", id);
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            JobDto job = new JobDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                            jobDtos.Add(job);
                        }
                        return jobDtos;
                    }
                }
            }
            catch (MySqlException)
            {
                throw new GetAllJobsFailedException("Unable to get jobs by categorie id, please try again");
            }
        }
    }
}
