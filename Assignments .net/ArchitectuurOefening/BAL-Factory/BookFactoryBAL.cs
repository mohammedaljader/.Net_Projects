using BAL;
using BAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_Factory
{
    public class BookFactoryBAL
    {
        public static IBookCollection bookCollection()
        {
            return new BookCollection();
        }

        public static IBook book(int id,string name , int age , string telephone, string email)
        {
            return new Book(id , name, age, telephone, email);
        }
    }
}
