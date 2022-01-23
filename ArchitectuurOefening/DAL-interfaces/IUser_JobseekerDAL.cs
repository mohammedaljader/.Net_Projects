using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces
{
    public interface IUser_JobseekerDAL
    {
        void Register(Job_SeekerDto job_Seeker);
        Job_SeekerDto Login(string username, string password);
        void EditProfile(Job_SeekerDto job_Seeker);
        List<Job_SeekerDto> GetAllJobSeerkers();
    }
}
