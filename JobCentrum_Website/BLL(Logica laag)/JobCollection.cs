using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_Factory;
using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using Exceptions.Job;

namespace BLL_Logica_laag_
{
    public class JobCollection : IJobCollection
    {
        #region declarations and relations
        public List<Job> Jobs;
        private readonly IJobDAL _jobDAL;
        private readonly IUser_companyDAL _userCollection;
        private readonly ICategorieDAL _categorieDAL;
        #endregion

        #region Constractors
        public JobCollection()
        {
            _jobDAL = JobDalFactory.JobDAL();
            _userCollection = AccountDalFactory.User_JobPublisher();
            _categorieDAL = CategorieDalFactory.categorieDAL();
            Jobs = new List<Job>();
            Jobs.Add(new Job(0, "", "", "","", 0, 0));
            Jobs.Add(new Job(111111, "", "", "", "", 1111, 1111));
            #region retrieve data from the database 
            foreach (JobDto jobDto in _jobDAL.GetAllJobs())
            {
                Job job = new Job(jobDto);
                Jobs.Add(job);
            }
            #endregion
        }
        #endregion

        #region Methodes
        public void AddJob(string job_name, string Job_description, string Job_image, string Job_location, int Categorie_ID, int Publisher_ID)
        {
            CompanyDto JobPublisherDto = _userCollection.FindJobPublisher(Publisher_ID);
            CategorieDto categoriedto = _categorieDAL.FindCategorie(Categorie_ID);
            if (JobPublisherDto != null && categoriedto != null)
            {
                //JobPublisher jobPublisher = new JobPublisher(JobPublisherDto.Id, JobPublisherDto.Fullname, JobPublisherDto.Username, JobPublisherDto.Password, JobPublisherDto.Email, JobPublisherDto.Telephone, JobPublisherDto.Address, JobPublisherDto.Company_Id, JobPublisherDto.Company_Name, JobPublisherDto.Company_Address);
                Job job = new Job(job_name, Job_description, Job_image, Job_location, Categorie_ID, Publisher_ID);
                Jobs.Add(job);
                _jobDAL.AddJob(job.ConvertToDto());
            }
        }

        public void DeleteJob(int id)
        {
            Job job = Jobs.FirstOrDefault(x => x.Job_Id == id);
            if(job != null)
            {
                Jobs.Remove(job);
                _jobDAL.DeleteJob(id);
            }
        }

        public IReadOnlyCollection<IJob> GetAllJobs()
        {
            List<JobDto> jobDtos = _jobDAL.GetAllJobs();
            Jobs.Clear();
            foreach (var dto in jobDtos)
            {
                Jobs.Add(new Job(dto));
            }
            return Jobs.AsReadOnly();
        }

        public void SearchForJob()
        {
            throw new NotImplementedException();
        }

        public IJob FindJob(int id)
        {
            Job job = Jobs.FirstOrDefault(x => x.Job_Id == id);
            if (job != null)
            {
                return job;
            }
            else
            {
                throw new FindingJobFailedException("Unable to find the job. The job is not exits!!");
            }
        }

        public IReadOnlyCollection<IJob> GetAllJobsByJobPublisher(int id)
        {
            List<JobDto> jobDtos = _jobDAL.GetAllJobByJobPublisher(id);
            if(jobDtos == null)
            {
                throw new GetAllJobsByJobPublisherFailedException("Unable to get jobs. The id of user is not exits");
            }
            Jobs.Clear();
            foreach (var dto in jobDtos)
            {
                Jobs.Add(new Job(dto));
            }
            return Jobs.AsReadOnly();
        }
        #endregion
    }
}
