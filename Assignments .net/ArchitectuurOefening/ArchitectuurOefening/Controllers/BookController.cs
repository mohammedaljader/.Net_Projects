using ArchitectuurOefening.Models;
using BAL_Factory;
using BAL_interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchitectuurOefening.Controllers
{
    public class BookController : Controller
    {
        private readonly List<BookModel> models;
        private readonly IBookCollection BookCollection;

        public BookController()
        {
            BookCollection = BookFactoryBAL.bookCollection();
            models = new List<BookModel>();
            models = GetAll();
        }

        // GET: BookController
        public ActionResult Index()
        {
            //Get All books            
            return View(models);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            //Find Book
            BookModel bookModel = models.SingleOrDefault(book => book.ID == id);
            return View(bookModel);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            //var list = new List<string>() { "Test", "Test1" };
           // ViewBag.list = list;
            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookModel bookModel)
        {
            try
            {
                //IBookCollection book = BookFactoryBAL.bookCollection();
                BookCollection.AddBook(bookModel.Name, bookModel.Age, bookModel.Telephone, bookModel.Email);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            //Find Book
            BookModel bookModel = models.FirstOrDefault(book => book.ID == id);
            return View(bookModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookModel bookModel)
        {
            try
            {
                IBook Bookje = BookFactoryBAL.book(bookModel.ID, bookModel.Name, bookModel.Age , bookModel.Telephone , bookModel.Email) ;
                Bookje.UpdateBook(Bookje.Id, Bookje.Name, Bookje.Age, Bookje.Telephone, Bookje.Email);
                #region Collection Class Way
                //BookCollection.UpdateBook(bookModel.ID, bookModel.Name, bookModel.Age, bookModel.Telephone, bookModel.Email);
                #endregion
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            //Find Book
            BookModel bookModel = models.FirstOrDefault(book => book.ID == id);
            return View(bookModel);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BookModel bookModel)
        {
            try
            {
                IBookCollection book = BookFactoryBAL.bookCollection();
                book.RemoveBook(id);
                models.Remove(bookModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<BookModel> GetAll()
        {
            var model = BookCollection.GetBooks();
            foreach (var item in model)
            {
                models.Add(new BookModel
                {
                    ID = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Telephone = item.Telephone,
                    Email = item.Email
                });
            }
            return models;
        }
    }
}
