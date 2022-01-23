using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.DTO_s
{
    public class JobDto
    {
        public int Job_Id { get; set; }
        public string Job_name { get; set; }
        public string Job_description { get; set; }
        public string Job_image { get; set; }
        public string Job_location { get; set; }
        public int Categorie_ID { get; set; }
        public int Publisher_ID { get; set; }

        public JobDto(int jobId, string jobName, string jobDescription, string jobImage, string job_location, int categorie_ID, int publisher_ID )
        {
            Job_Id = jobId;
            Job_name = jobName;
            Job_description = jobDescription;
            Job_image = jobImage;
            Job_location = job_location;
            Categorie_ID = categorie_ID;
            Publisher_ID = publisher_ID;
        }
        public JobDto(string jobName, string jobDescription, string jobImage, string job_location)
        {
            Job_name = jobName;
            Job_description = jobDescription;
            Job_image = jobImage;
            Job_location = job_location;
        }
    }
}
