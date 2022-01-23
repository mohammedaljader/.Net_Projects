using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions.JobApplication;

namespace DAL.DataAccess
{
    public class JobApplicationDAL : IJobApplicationDAL
    {
        public void Apply_For_Job(JobApplicationDto jobApplication)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("insert into jobapplication(ApplyID, JobSeeker_ID,Job_ID,motivation, cv,Datum_Apply) values(DEFAULT, @JobSeeker_ID,@Job_ID, @motivation,@cv,DEFAULT)", conn))
                    {
                        cmd.Parameters.AddWithValue("@JobSeeker_ID", jobApplication.Job_Seeker_Id);
                        cmd.Parameters.AddWithValue("@Job_ID", jobApplication.Job_Id);
                        cmd.Parameters.AddWithValue("@motivation", jobApplication.Motivation);
                        cmd.Parameters.AddWithValue("@cv", jobApplication.CV);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AddingApplicationFailedException("Unale to Apply for this job. Try again please");
            }
        }

        public void DeleteApplication(int id)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("Delete from jobapplication where ApplyID=@ApplyID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ApplyID", id);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new RemovingApplicationFailedException("Unable to remove the application. Try again please");
            }
        }

        public List<JobApplicationDto> GetAllApplicationsByJobSeeker(int id)
        {
            try
            {
                List<JobApplicationDto> Applied_Jobs = new List<JobApplicationDto>();
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM jobapplication WHERE JobSeeker_ID = @JobSeeker_ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@JobSeeker_ID", id);
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            JobApplicationDto jobApplicationDto = new JobApplicationDto(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                            Applied_Jobs.Add(jobApplicationDto);
                        }
                    }
                }
                return Applied_Jobs;
            }
            catch (MySqlException)
            {
                throw new GetingAllApplicationsByJobSeekerFailedException("Unable to get all applications for you. Try again please");
            }
        }

    
        public List<JobApplicationDto> GetAllApplications()
        {
            try
            {
                List<JobApplicationDto> Applications = new List<JobApplicationDto>();
                // string query = "SELECT DISTINCT Job_title, Job_description, Job_image, Job_location FROM job inner JOIN jobapplication on job.Job_ID = jobapplication.Job_ID INNER JOIN jobseeker on jobseeker.JobSeeker_ID = jobapplication.JobSeeker_ID";
                using (MySqlConnection conn = new MySqlConnection(DBConnection.ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM jobapplication ", conn))
                    {
                        conn.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            JobApplicationDto application = new JobApplicationDto(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4));
                            Applications.Add(application);
                        }
                    }
                }
                return Applications;
            }
            catch (MySqlException)
            {
                throw new GetingAllApplicationsFailedException("Unable to get the applications, try again please");
            }
        }
    }
}
