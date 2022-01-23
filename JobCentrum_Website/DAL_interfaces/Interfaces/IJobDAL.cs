using DAL_interfaces.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.Interfaces
{
    public interface IJobDAL
    {
        List<JobDto> GetAllJobs();
        List<JobDto> GetAllJobByJobPublisher(int id);
        List<JobDto> GetAllJobsByCategorieId(int id);
        void AddJob(JobDto job);
        void DeleteJob(int id);
        void EditJob(JobDto job);
        JobDto FindJobById(int id);
    }
}
