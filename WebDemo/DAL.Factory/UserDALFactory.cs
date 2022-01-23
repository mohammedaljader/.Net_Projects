﻿using DAL.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public static class UserDALFactory
    {
        public static IUserDAL UserFactory()
        {
            return new UserDAL();
        }
    }
}
