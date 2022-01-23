using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_interfaces
{
    public interface IJobSeeker : IUser
    {
        public int Seeker_Id { get; set; }
        public int Experience { get; set; }
        public string Hobby { get; set; }

        public void ApplyForJob();

        public void RemoveApplication();
    }
}
