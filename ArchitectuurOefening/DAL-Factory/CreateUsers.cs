using DAL;
using DAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Factory
{
    public static class CreateUsers
    {
        public static IUser_JobseekerDAL User_JobSeeker()
        {
            return new UserJobSeekerDAL();
        }

        public static IUser_companyDAL User_JobPublisher()
        {
            return new UserJobPublisherDAL();
        }
    }
}
