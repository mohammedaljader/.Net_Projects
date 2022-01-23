using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_interfaces
{
    public interface IUserCollection
    {
        bool RegisterJobSeeker(string fullname, string username, string password, string email, string telephone, string address, int experience, string hobby);
        IJobSeeker LoginJobSeeker(/*IJobSeeker jobSeeker*/ string username, string password);
        bool RegisterJobPublisher(string fullname, string username, string password, string email, string telephone, string address, string companyName, string companyAddress);
        IJobPublisher LoginJobPublisher(/*IJobPublisher jobPublisher*/ string username, string password);
    }
}
