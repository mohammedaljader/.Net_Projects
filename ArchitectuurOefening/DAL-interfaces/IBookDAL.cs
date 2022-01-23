using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces
{
    public interface IBookDAL
    {
        List<BookDto> GetAllBooks();
        void CreateBook(BookDto book);
        void RemoveBook(int id);
        void UpdateBook(BookDto book);
        BookDto FindBookByID(int id);
    }
}
