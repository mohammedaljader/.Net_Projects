using BAL_interfaces;
using DAL_Factory;
using DAL_interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BookCollection : IBookCollection
    {
        #region 
        private readonly List<Book> books;
        private readonly IBookDAL bookDal;
        #endregion

        public BookCollection()
        {
            bookDal = CreateBook.Create_Book();
            books = new List<Book>();
            #region add HardCode to the list Way 
            books.Add(new Book(10000, "Test", 12, "1245566", "mmm@gmail.com"));
            #endregion
            #region Get Data from Database Way
            foreach (BookDto bookdto in bookDal.GetAllBooks())
            {
                Book book = new Book(bookdto);
                books.Add(book);
            }
            #endregion
        }

        public void AddBook(string name, int age, string telephone, string email)
        {
            Book book = new Book(name , age , telephone , email);
            books.Add(book);
            bookDal.CreateBook(book.ConvertToDto());  
        }

        public void RemoveBook(int id)
        {
            Book book = books.FirstOrDefault(book => book.Id == id);
            if (book != null)
            {
                books.Remove(book);
                bookDal.RemoveBook(id);
            }
        }

        public IReadOnlyCollection<IBook> GetBooks()
        {
            List<BookDto> BooksDto = bookDal.GetAllBooks();
            books.Clear();
            foreach (var dto in BooksDto)
            {
                books.Add(new Book(dto));
            }
            return books.AsReadOnly();
        }

        #region Update in collection class Way
        //public void UpdateBook(int id, string name, int age, string telephone, string email)
        //{
        //    Book book = new Book(id, name, age, telephone, email);
        //    bookDal.UpdateBook(book.ConvertToDto());
        //}
        #endregion
    }
}
