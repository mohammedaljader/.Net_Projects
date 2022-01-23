using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using ShopOnline.Models;
using Models.DataModels;
using Exceptions.Product;

namespace ShopOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductLogic productLogic;
        private readonly IReviewLogic reviewLogic;

        public ProductController(IProductContext productContext, IReviewContext reviewContext)
        {
            productLogic = new ProductLogic(productContext);
            reviewLogic = new ReviewLogic(reviewContext);
        }

        private List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                products = productLogic.GetAll();
                foreach (Product product in products)
                {
                    product.reviews = reviewLogic.GetAllByProduct(product);
                }
                return products;
            }
            catch (Exception)
            {
                return products;
            }
        }

        public IActionResult Info(int id)
        {
            Product product = GetAllProducts().Find(foundProduct => foundProduct.ID == id);
            if (product == null)
            {
                return RedirectToAction("Index", "Shop");
            }
            ViewBag.Reviews = reviewLogic.GetAllByProduct(product);
            ViewBag.Product = product;
            return View();
        }

        public IActionResult ProductPanel()
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
            {
                ViewBag.Products = GetAllProducts();
                return View();
            }
            return RedirectToAction("Index", "Shop");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(int id, ProductViewModel model)
        {
            User loggedInUser = HttpContext.Session.GetUser();
            Product product = GetAllProducts().Find(foundProduct => foundProduct.ID == id);
            ViewBag.product = product;
            if (loggedInUser.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        productLogic.EditProduct(new Product
                        {
                            ID = id,
                            Name = model.Name,
                            Description = model.Description,
                            ImageURL = model.ImageURL,
                            Price = model.Price,

                        });
                        return RedirectToAction("ProductPanel", "Product");
                    }
                    catch (UpdatingProductFailedException)
                    {
                        ModelState.AddModelError("", "Editing product failed, Try again.");
                        return RedirectToAction("ProductPanel", "Product");
                    }
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProduct(int id)
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
            {
                try
                {
                    Product product = GetAllProducts().Find(foundProduct => foundProduct.ID == id);
                    if (product.reviews.Any())
                    {
                        foreach (Review review in product.reviews)
                        {
                            reviewLogic.RemoveReview(review);
                        }
                    }
                    productLogic.RemoveProduct(product);
                    return RedirectToAction("ProductPanel", "Product");
                }
                catch (Exception)
                {
                    return RedirectToAction("ProductPanel", "Product");
                }
            }
            return RedirectToAction("ProductPanel", "Product");
        }

        public ActionResult AddProduct()
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
            {
                return View();
            }
            return RedirectToAction("ProductPanel", "Product");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(ProductViewModel model)
        {
            User loggedInUser = HttpContext.Session.GetUser();
            if (loggedInUser.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        productLogic.AddProduct(new Product
                        {
                            Name = model.Name,
                            Description = model.Description,
                            ImageURL = model.ImageURL,
                            Price = model.Price,

                        });
                        return RedirectToAction("ProductPanel", "Product");
                    }
                    catch (AddingProductFailedException)
                    {
                        ModelState.AddModelError("", "Adding product failed, Try again.");
                        return RedirectToAction("ProductPanel", "Product");
                    }
                }
            }
            return View();
        }
    }
}
