using DAL;
using DAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Factory
{
    public class CreateBook
    {
        public static IBookDAL Create_Book()
        {
            return new BookDAL();
        }
    }
}
