using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface IJob
    {
        public int Job_Id { get;}
        public string Job_name { get;}
        public string Job_description { get;}
        public string Job_image { get;}
        public string Job_location { get;}
        public int Categorie_ID { get;}
        public int Publisher_ID { get;}

        void EditJob(int jobId, string jobName, string jobDescription, string jobImage, string job_location, int categorie_id, int publisher_id);
    }
}
