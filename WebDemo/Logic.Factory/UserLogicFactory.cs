using Logic.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Factory
{
    public static class UserLogicFactory
    {
        public static IUserCollection UserCollection()
        {
            return new UserCollection();
        }

    }
}
