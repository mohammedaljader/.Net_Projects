using BAL;
using BAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_Factory
{
    public static class AccountFactory
    {
        public static IUserCollection UserCollection()
        {
            return new UserCollection();
        }
    }
}
