using BLL_interfaces;
using BLL_Logica_laag_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Factory
{
    public static class JobFactory
    {
        public static IJobCollection JobCollection()
        {
            return new JobCollection();
        }
        public static IJob Job(int jobId, string jobName, string jobDescription, string jobImage, string job_location, int categorie_id, int publisher_id)
        {
            return new Job(jobId, jobName, jobDescription, jobImage, job_location, categorie_id, publisher_id);
        }
    }
}
