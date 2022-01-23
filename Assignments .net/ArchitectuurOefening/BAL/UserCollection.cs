using BAL_interfaces;
using DAL_Factory;
using DAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Hashing;

namespace BAL
{
    public class UserCollection : IUserCollection
    {
        private readonly IUser_JobseekerDAL _JobseekerDAL;
        private List<User> Users;
        private readonly IUser_companyDAL _CompanyDAL;

        public UserCollection()
        {
            _JobseekerDAL = CreateUsers.User_JobSeeker();
            _CompanyDAL = CreateUsers.User_JobPublisher();
            Users = new List<User>();
            Users.Add(new JobSeeker(11111, "", "", "", "", "", "", 11111, 11, ""));
            Users.Add(new JobPublisher(1111, "", "", "", "", "", "", 11111, "", ""));

        }

        public IJobPublisher LoginJobPublisher(string username, string password)
        {
            JobPublisher jobPublisher = null;
            if (username != string.Empty && password != string.Empty && password.Length > 6 )
            {
                CompanyDto dto =  _CompanyDAL.Login(username , /*new HashPassword().PasswordHasher(password)*/ password);
                if(dto != null)
                {
                    jobPublisher = new JobPublisher(dto.Id, dto.Fullname, dto.Username, dto.Password, dto.Email, dto.Telephone, dto.Address, dto.Company_Id, dto.Company_Name, dto.Company_Address);
                    return jobPublisher;
                }
            }
            return jobPublisher;
        }

        public IJobSeeker LoginJobSeeker(string username, string password)
        {
            JobSeeker jobSeeker = null;
            if (username != string.Empty && password != string.Empty && password.Length > 6)
            {
                Job_SeekerDto dto = _JobseekerDAL.Login(username, /*new HashPassword().PasswordHasher(password)*/ password);
                if(dto != null)
                {
                    jobSeeker = new JobSeeker(dto.Id, dto.Fullname, dto.Username, dto.Password, dto.Email, dto.Telephone, dto.Address, dto.Seeker_Id, dto.Experience, dto.Hobby);
                    return jobSeeker;
                } 
            }
            return jobSeeker;
        }

        public bool RegisterJobPublisher(string fullname, string username, string password, string email, string telephone, string address, string companyName, string companyAddress)
        {
            Users = GetAllUsers(true); //True means that this account is for jobpublisher
            if (Users.Any(x => x.Username == username) == false && Users.Any(y => y.Email == email) == false)
            {
                password = new HashPassword().PasswordHasher(password);
                JobPublisher jobPublisher = new JobPublisher(fullname, username, password, email, telephone, address, companyName, companyAddress);
               // jobPublisher.Password = new HashPassword().PasswordHasher(password);
                Users.Add(jobPublisher);
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
            Users = GetAllUsers(false); //false means that this account is for jobSeeker
            if (Users.Any(x => x.Username == username) == false && Users.Any(y => y.Email == email) == false)
            {
                //password = new HashPassword().PasswordHasher(password);
                JobSeeker jobSeeker = new JobSeeker(fullname, username, password, email, telephone, address, experience, hobby);
                jobSeeker.Password = new HashPassword().PasswordHasher(password);
                Users.Add(jobSeeker);
                _JobseekerDAL.Register(jobSeeker.ConvertJobSeekerToDto());
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<User> GetAllUsers(bool IsJobPublisher)
        {
            if(IsJobPublisher == true)
            {
                List<CompanyDto> jobPublishers = _CompanyDAL.GetAllJobPublishers();
                Users.Clear();
                foreach (var dto in jobPublishers)
                {
                    Users.Add(new JobPublisher(dto));
                }
                return Users;
            }
            else
            {
                List<Job_SeekerDto> GetAllJobSeekers = _JobseekerDAL.GetAllJobSeerkers();
                Users.Clear();
                foreach (var dto in GetAllJobSeekers)
                {
                    Users.Add(new JobSeeker(dto));
                }
                return Users;
            }
            
        }
    }
}
