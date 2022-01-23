using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface IJobCollection
    {
       void AddJob(string job_name, string Job_description, string Job_image ,string Job_location, int Categorie_ID , int Publisher_ID);
       void DeleteJob(int id);
       IReadOnlyCollection<IJob> GetAllJobs();
       IReadOnlyCollection<IJob> GetAllJobsByJobPublisher(int id);
       void SearchForJob();
       IJob FindJob(int id);
    }
}
