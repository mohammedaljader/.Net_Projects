using DAL.DataAccess;
using DAL_interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Factory
{
    public static class AccountDalFactory
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
