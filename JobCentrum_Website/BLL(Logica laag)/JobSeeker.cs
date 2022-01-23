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
    public class JobSeeker : User, IJobSeeker
    {

        #region declarations and relations
        private readonly IUser_JobseekerDAL _JobseekerDAL;
        private readonly IJobApplicationDAL _JobApplicationDAL;
        private List<JobApplication> jobApplications;
        #endregion

        #region Properties
        public int Seeker_Id { get; }
        public int Experience { get; }
        public string Hobby { get; }
        #endregion

        #region Constracturs 
        public JobSeeker():base()
        {
            _JobApplicationDAL = JobApplicationFactory.JobApplicationDAL();
            jobApplications = new List<JobApplication>();
            jobApplications = GetAllApplications();
        }

        public JobSeeker(int userId, string fullname, string username, string password, string email, string telephone, string address, int seekerId, int experience, string hobby) : base(userId, fullname, username, password, email, telephone, address)
        {
            Seeker_Id = seekerId;
            Experience = experience;
            Hobby = hobby;
            _JobseekerDAL = AccountDalFactory.User_JobSeeker();
        }
        public JobSeeker(string fullname, string username, string password, string email, string telephone, string address, int experience, string hobby) : base(fullname, username, password, email, telephone, address)
        {
            this.Experience = experience;
            this.Hobby = hobby;
        }

        public JobSeeker(Job_SeekerDto user)
        : this(user.Id, user.Fullname, user.Username, user.Password, user.Email, user.Telephone, user.Address, user.Seeker_Id, user.Experience, user.Hobby)
        {

        }

        public Job_SeekerDto ConvertJobSeekerToDto()
        {
            return new Job_SeekerDto(this.Id, this.Fullname, this.Username, this.Password, this.Email, this.Telephone, this.Address, this.Seeker_Id, this.Experience, this.Hobby);
        }

        #endregion

        #region Methodes
        public void ApplyForJob(string cv , string motivation , int job_id , int jobseeker_id)
        {
            JobApplication jobApplication = new JobApplication(motivation, cv, job_id, jobseeker_id);
            jobApplications.Add(jobApplication);
            _JobApplicationDAL.Apply_For_Job(jobApplication.ConvertJobApplicationToDto());
        }

        public void RemoveApplication(int id)
        {
            jobApplications = GetAllApplications();
            var Application = jobApplications.FirstOrDefault(x => x.Apply_Id == id);
            if(Application != null)
            {
                _JobApplicationDAL.DeleteApplication(id);
                jobApplications.Remove(Application);
            }
        }
        public IReadOnlyCollection<IJobApplication> GetAllApplicationsByJobSeeker(int id)
        {
            List<JobApplicationDto> jobDtos = _JobApplicationDAL.GetAllApplicationsByJobSeeker(id);
            List<JobApplication> Jobs = new List<JobApplication>();
            foreach (var dto in jobDtos)
            {
                Jobs.Add(new JobApplication(dto));
            }
            return Jobs.AsReadOnly();
        }

        private List<JobApplication> GetAllApplications()
        {
            List<JobApplicationDto> ApplicationsDal = _JobApplicationDAL.GetAllApplications();
            jobApplications.Clear();
            foreach (var item in ApplicationsDal)
            {
                jobApplications.Add(new JobApplication(item));
            }
            return jobApplications;
        }

        public void ChangeProfileJobSeeker(int id, string fullname, string username, string password, string email, string telephone, string address, int jobseeker_id, int experience, string hobby)
        {
            JobSeeker jobSeeker = new JobSeeker(id, fullname, username, password, email, telephone, address, jobseeker_id, experience, hobby);
            _JobseekerDAL.EditProfile(jobSeeker.ConvertJobSeekerToDto());
        }
        #endregion
    }
}
