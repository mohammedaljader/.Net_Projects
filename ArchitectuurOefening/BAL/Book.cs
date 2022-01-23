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
    public class Book : IBook
    {
        private readonly IBookDAL bookDal;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }

       // public ICollection<Book> Books { get; set; } = new List<Book>();


        public Book(int id, string name, int age, string telephone, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Telephone = telephone;
            this.Email = email;
            bookDal = CreateBook.Create_Book();
        }
        public Book(string name, int age, string telephone, string email)
        {
            this.Name = name;
            this.Age = age;
            this.Telephone = telephone;
            this.Email = email;
        }

        public Book(BookDto bookDto)
          : this(bookDto.Id, bookDto.Name, bookDto.Age, bookDto.Telephone, bookDto.Email)
        {

        }

        public BookDto ConvertToDto()
        {
            return new BookDto(this.Id, this.Name, this.Age, this.Telephone, this.Email);
        }

        public void UpdateBook(int id, string name, int age, string telephone, string email)
        {
            Book book = new Book(id, name, age, telephone, email);
            bookDal.UpdateBook(book.ConvertToDto());
        }

        //public List<Book> Books(List<BookDto> bookDtos)
        //{
        //    List<Book> books = new List<Book>();
        //    foreach (var item in bookDtos)
        //    {
        //        books.Add(new Book(item));
        //    }
        //    return books;
        //}
    }
}
