using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BAL_interfaces
{
    public interface IBook
    {
        int Age { get; set; }
        string Email { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string Telephone { get; set; }

        void UpdateBook(int id, string name, int age, string telephone, string email);

    }
}