using BAL_interfaces;
using DAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class JobSeeker : User , IJobSeeker
    {
        public int Seeker_Id { get; set; }
        public int Experience { get; set; }
        public string Hobby { get; set; }

        public JobSeeker (int userId, string fullname, string username, string password, string email, string telephone, string address, int seekerId, int experience, string hobby) : base(userId, fullname, username, password, email, telephone, address)
        {
            Seeker_Id = seekerId;
            Experience = experience;
            Hobby = hobby;
        }
        public JobSeeker(string fullname, string username, string password, string email, string telephone, string address, int experience, string hobby) : base( fullname , username , password , email , telephone ,address)
        {
            this.Experience = experience;
            this.Hobby = hobby;
        }

        public JobSeeker(Job_SeekerDto user)
        : this(user.Id , user.Fullname , user.Username , user.Password , user.Email , user.Telephone , user.Address, user.Seeker_Id , user.Experience , user.Hobby)
        {

        }

        public Job_SeekerDto ConvertJobSeekerToDto()
        {
            return new Job_SeekerDto(this.Id, this.Fullname, this.Username, this.Password, this.Email, this.Telephone, this.Address, this.Seeker_Id, this.Experience, this.Hobby);
        }

        public void ApplyForJob()
        {
            throw new NotImplementedException();
        }

        public void RemoveApplication()
        {
            throw new NotImplementedException();
        }
    }
}
