using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_Factory;
using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;

namespace BLL_Logica_laag_
{
    public class Job : IJob
    {
        #region declarations and relations
        private IJobDAL _jobDAL;
        #endregion

        #region Properties
        public int Job_Id { get; }
        public string Job_name { get;}
        public string Job_description { get;}
        public string Job_image { get; }
        public string Job_location { get;}
        public int Categorie_ID { get;}
        public int Publisher_ID { get;}
        #endregion

        #region Constractors
        public Job(int jobId, string jobName, string jobDescription, string jobImage, string job_location , int categorie_id, int publisher_id)
        {
            Job_Id = jobId;
            Job_name = jobName;
            Job_description = jobDescription;
            Job_image = jobImage;
            Job_location = job_location;
            Categorie_ID = categorie_id;
            Publisher_ID = publisher_id;
            _jobDAL = JobDalFactory.JobDAL();
        }
        public Job(string jobName, string jobDescription, string jobImage, string job_location, int categorie_id, int publisher_id)
        {
            Job_name = jobName;
            Job_description = jobDescription;
            Job_image = jobImage;
            Job_location = job_location;
            Categorie_ID = categorie_id;
            Publisher_ID = publisher_id;
        }

        public Job(JobDto jobDto)
           : this(jobDto.Job_Id, jobDto.Job_name, jobDto.Job_description, jobDto.Job_image,jobDto.Job_location , jobDto.Categorie_ID , jobDto.Publisher_ID)
        {

        }

        public JobDto ConvertToDto()
        {
            return new JobDto(this.Job_Id, this.Job_name, this.Job_description, this.Job_image , this.Job_location , this.Categorie_ID , this.Publisher_ID);
        }
        #endregion

        #region Methodes
        public void EditJob(int jobId, string jobName, string jobDescription, string jobImage, string job_location, int categorie_id, int publisher_id)
        {
            Job job = new Job(jobId, jobName, jobDescription, jobImage, job_location, categorie_id, publisher_id);
            _jobDAL.EditJob(job.ConvertToDto());
        }
        #endregion
    }
}
