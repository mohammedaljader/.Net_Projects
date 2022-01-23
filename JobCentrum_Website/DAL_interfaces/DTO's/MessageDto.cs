using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.DTO_s
{
    public class MessageDto
    {
        public int Message_Id { get; set; }
        public string Message_subject { get; set; }
        public string Message_Text { get; set; }
        public Job_SeekerDto JobSeeker_Id { get; set; }
        public CompanyDto Publisher_Id { get; set; }
        public JobDto Job_Id { get; set; }

        public MessageDto(int messageId, string messageSubject, string messageText, int jobseeker_id , int publisher_id, int job_id)
        {
            Message_Id = messageId;
            Message_subject = messageSubject;
            Message_Text = messageText;
            JobSeeker_Id.Seeker_Id = jobseeker_id;
            Publisher_Id.Company_Id = publisher_id;
            Job_Id.Job_Id = job_id;
        }
    }
}
