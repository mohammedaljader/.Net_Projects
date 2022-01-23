using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_Factory;
using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using BLL_Logica_laag_.HashingData;
using Exceptions.JobPublisher;
using Exceptions.JobSeeker;

namespace BLL_Logica_laag_
{
    public class UserCollection : IUserCollection
    {
        #region declarations and relations
        private readonly IUser_JobseekerDAL _JobseekerDAL;
        private List<JobPublisher> JobPublishers;
        private List<JobSeeker> JobSeekers;
        private readonly IUser_companyDAL _CompanyDAL;
        #endregion

        #region Contractors
        public UserCollection()
        {
            _JobseekerDAL = AccountDalFactory.User_JobSeeker();
            _CompanyDAL = AccountDalFactory.User_JobPublisher();
            JobPublishers = new List<JobPublisher>();
            JobSeekers = new List<JobSeeker>();
            JobSeekers.Add(new JobSeeker(11111, "", "", "", "", "", "", 11111, 11, ""));
            JobPublishers.Add(new JobPublisher(1111, "", "", "", "", "", "", 11111, "", ""));

        }
        #endregion

        #region Methodes
        public IJobPublisher LoginJobPublisher(string username, string password)
        {
            if (username != string.Empty && password != string.Empty)
            {
                JobPublishers = GetAllJobPublishers();
                JobPublisher jobPublisher = JobPublishers.Find(x => x.Username == username && x.Password == password);
                if(jobPublisher != null)
                {
                    //Data from database
                    CompanyDto dto = _CompanyDAL.Login(username, password);
                    if (dto != null)
                    {
                        jobPublisher = new JobPublisher(dto.Id, dto.Fullname, dto.Username, dto.Password, dto.Email, dto.Telephone, dto.Address, dto.Company_Id, dto.Company_Name, dto.Company_Address);
                        return jobPublisher;
                    }
                }
            }
            return null;
        }
        public IJobSeeker LoginJobSeeker(string username, string password)
        {
            if (username != string.Empty && password != string.Empty)
            {
                JobSeekers = GetAllJobSeekers();
                JobSeeker jobSeeker = JobSeekers.Find(x => x.Username == username && x.Password == password);
                if(jobSeeker != null)
                {
                    //Data from database
                    Job_SeekerDto dto = _JobseekerDAL.Login(username, password);
                    if (dto != null)
                    {
                        jobSeeker = new JobSeeker(dto.Id, dto.Fullname, dto.Username, dto.Password, dto.Email, dto.Telephone, dto.Address, dto.Seeker_Id, dto.Experience, dto.Hobby);
                        return jobSeeker;
                    }
                }  
            }
            return null;
        }
        public bool RegisterJobPublisher(string fullname, string username, string password, string email, string telephone, string address, string companyName, string companyAddress)
        {
            JobPublishers = GetAllJobPublishers();
            if (JobPublishers.Any(x => x.Username == username) == false && JobPublishers.Any(y => y.Email == email) == false)
            {
                JobPublisher jobPublisher = new JobPublisher(fullname, username, password, email, telephone, address, companyName, companyAddress);
                //jobPublisher.Password = HashData.PasswordHasher(password);
                JobPublishers.Add(jobPublisher);
                _CompanyDAL.Register(jobPublisher.ConvertJobPublisherToDto());
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool RegisterJobSeeker(string fullname, string username, string password, string email, string telephone, string address, int experience, string hobby)
        {
            JobSeekers = GetAllJobSeekers(); 
            if (JobSeekers.Any(x => x.Username == username) == false && JobSeekers.Any(y => y.Email == email) == false)
            {
                JobSeeker jobSeeker = new JobSeeker(fullname, username, password, email, telephone, address, experience, hobby);
                //jobSeeker.Password = HashData.PasswordHasher(password);
                JobSeekers.Add(jobSeeker);
                _JobseekerDAL.Register(jobSeeker.ConvertJobSeekerToDto());
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<JobPublisher> GetAllJobPublishers()
        {
            List<CompanyDto> jobPublishers = _CompanyDAL.GetAllJobPublishers();
            JobPublishers.Clear();
            foreach (var dto in jobPublishers)
            {
                JobPublishers.Add(new JobPublisher(dto));
            }
            return JobPublishers;
        }
        public List<JobSeeker> GetAllJobSeekers()
        {
            List<Job_SeekerDto> GetAllJobSeekers = _JobseekerDAL.GetAllJobSeerkers();
            JobSeekers.Clear();
            foreach (var dto in GetAllJobSeekers)
            {
                JobSeekers.Add(new JobSeeker(dto));
            }
            return JobSeekers;
        }
        #endregion

        #region Methodes Updating Profile Moved to User class
        //public void ChangeProfileJobSeeker(int id, string fullname, string username, string password, string email, string telephone, string address, int jobseeker_id, int experience, string hobby)
        //{
        //    JobSeekers = GetAllJobSeekers();
        //    JobSeeker jobSeeker = JobSeekers.FirstOrDefault(x => x.Username == username && x.Email == email);
        //    jobSeeker = new JobSeeker(id, fullname, username, password, email, telephone, address, jobseeker_id, experience, hobby);
        //    _JobseekerDAL.EditProfile(jobSeeker.ConvertJobSeekerToDto());
        //}
        //public void ChangeProfileJobPublisher(int id, string fullname, string username, string password, string email, string telephone, string address, int jobPublisher_id, string companyName, string companyAddress)
        //{
        //    JobPublishers = GetAllJobPublishers();
        //    JobPublisher jobPublisher = JobPublishers.FirstOrDefault(x => x.Username == username && x.Email == email);
        //    jobPublisher = new JobPublisher(id, fullname, username, password, email, telephone, address, jobPublisher_id, companyName, companyAddress);
        //    _CompanyDAL.EditProfile(jobPublisher.ConvertJobPublisherToDto());
        //}
        #endregion
    }
}
