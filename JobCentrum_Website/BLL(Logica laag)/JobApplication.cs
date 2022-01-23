using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_interfaces.DTO_s;

namespace BLL_Logica_laag_
{
    public class JobApplication : IJobApplication
    {

        #region Properties
        public int Apply_Id { get; }
        public string Motivation { get; }
        public string CV { get; }
        public DateTime Apply_date { get; }
        public int Job_id { get;  }
        public int JobSeeker_id { get; }
        #endregion

        #region Constractors
        public JobApplication(string motivation, string cv, int job_id , int jobseeker_id)
        {
            Motivation = motivation;
            CV = cv;
            Job_id = job_id;
            JobSeeker_id = jobseeker_id;
        }
        public JobApplication(int apply_id,string motivation, string cv, int job_id, int jobseeker_id)
        {
            Apply_Id = apply_id;
            Motivation = motivation;
            CV = cv;
            Job_id = job_id;
            JobSeeker_id = job_id;
        }
        public JobApplication(JobApplicationDto jobApplicationDto): this(jobApplicationDto.Apply_Id, jobApplicationDto.Motivation, jobApplicationDto.CV, jobApplicationDto.Job_Id, jobApplicationDto.Job_Seeker_Id)
        {

        }
        public JobApplicationDto ConvertJobApplicationToDto()
        {
            return new JobApplicationDto(this.Motivation , this.CV , this.JobSeeker_id , this.Job_id);
        }
        #endregion
    }
}
