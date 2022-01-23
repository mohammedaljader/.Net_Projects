using BLL_interfaces;
using BLL_Logica_laag_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Factory
{
    public static class AccountFactory
    {
        public static IUserCollection UserCollection()
        {
            return new UserCollection();
        }

        public static IJobPublisher jobPublisherFactory(int id, string fullname, string username, string password, string email, string telephone, string address, int jobPublisher_id, string companyName, string companyAddress)
        {
            return new JobPublisher(id, fullname, username, password, email, telephone, address, jobPublisher_id, companyName, companyAddress);
        }

        public static IJobSeeker jobSeekerFactory(int id, string fullname, string username, string password, string email, string telephone, string address, int jobseeker_id, int experience, string hobby)
        {
            return new JobSeeker(id, fullname, username, password, email, telephone, address, jobseeker_id, experience, hobby);
        }
    }
}
