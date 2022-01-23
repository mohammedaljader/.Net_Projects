using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface IJobSeeker : IUser
    {
        public int Seeker_Id { get; }
        public int Experience { get; }
        public string Hobby { get; }

        void ApplyForJob(string cv, string motivation, int job_id, int jobseeker_id);
        IReadOnlyCollection<IJobApplication>GetAllApplicationsByJobSeeker(int id);
        void RemoveApplication(int id);

        void ChangeProfileJobSeeker(int id, string fullname, string username, string password, string email, string telephone, string address, int jobseeker_id, int experience, string hobby);
    }
}
