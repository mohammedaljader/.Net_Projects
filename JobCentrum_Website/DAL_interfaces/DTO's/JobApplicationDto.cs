using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.DTO_s
{
    public class JobApplicationDto
    {
        public int Apply_Id { get; set; }
        public int Job_Seeker_Id { get; set; }
        public int Job_Id { get; set; }
        public string Motivation { get; set; }
        public string CV { get; set; }

        public JobApplicationDto(int applyId, int job_seeker_Id, int job_ID, string motivation, string cv)
        {
            Apply_Id = applyId;
            Job_Seeker_Id = job_seeker_Id;
            Job_Id = job_ID;
            Motivation = motivation;
            CV = cv;
        }
        public JobApplicationDto(string motivation, string cv, int job_seeker_Id, int job_ID)
        {
            Job_Seeker_Id = job_seeker_Id;
            Job_Id = job_ID;
            Motivation = motivation;
            CV = cv;
        }
    }
}
