using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductLogic productLogic;
        private readonly ICartLogic cartLogic;

        public CartController(IProductContext productContext, ICartContext cartContext)
        {
            productLogic = new ProductLogic(productContext);
            cartLogic = new CartLogic(cartContext);
        }

        public IActionResult Index()
        {
            User loggedInUser = HttpContext.Session.GetUser();
            ViewBag.Cart = cartLogic.GetCartByUser(loggedInUser);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            try
            {
                User loggedInUser = HttpContext.Session.GetUser();
                cartLogic.AddToCart(productLogic.GetProductById(id), loggedInUser);
                HttpContext.Session.UpdateUser(loggedInUser);
                return RedirectToAction("Index", "Shop");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Shop");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                User loggedInUser = HttpContext.Session.GetUser();
                cartLogic.RemoveFromCart(productLogic.GetProductById(id), loggedInUser);
                HttpContext.Session.UpdateUser(loggedInUser);
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Cart");
            }
        }
    }
}
