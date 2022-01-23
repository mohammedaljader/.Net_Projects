using DAL_interfaces.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.Interfaces
{
    public interface IJobApplicationDAL 
    {
        void Apply_For_Job(JobApplicationDto jobApplication);
        void DeleteApplication(int id);
        List<JobApplicationDto> GetAllApplicationsByJobSeeker(int id);
        List<JobApplicationDto> GetAllApplications();
    }
}
