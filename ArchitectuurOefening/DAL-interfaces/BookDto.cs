using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<BookDto> Books { get; set; }

        public BookDto(int id, string name, int age, string telephone, string email, List<BookDto> books )
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Telephone = telephone;
            this.Email = email;
            this.Books = books;
        }

        public BookDto(int id, string name, int age, string telephone, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.Telephone = telephone;
            this.Email = email;
        }
    }
}
