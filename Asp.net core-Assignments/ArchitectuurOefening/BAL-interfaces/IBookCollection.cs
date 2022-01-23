using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL_interfaces
{
    public interface IBookCollection
    {
        void AddBook(string name, int age, string telephone, string email);
        void RemoveBook(int id);
        IReadOnlyCollection<IBook> GetBooks();
        //void UpdateBook(int id, string name, int age, string telephone, string email);
    }
}
