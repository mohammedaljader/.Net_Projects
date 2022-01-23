﻿using DAL.DataAccess;
using DAL_interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Factory
{
    public static class JobApplicationFactory
    {
        public static IJobApplicationDAL JobApplicationDAL()
        {
            return new JobApplicationDAL();
        }
    }
}
