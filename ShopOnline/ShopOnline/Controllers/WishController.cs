using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Wishes;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace ShopOnline.Controllers
{
    public class WishController : Controller
    {
        private readonly IProductLogic productLogic;
        private readonly IWishLogic wishLogic;

        public WishController(IProductContext productContext, IWishContext wishContext)
        {
            productLogic = new ProductLogic(productContext);
            wishLogic = new WishLogic(wishContext);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToWishes(int id)
        {
            try
            {
                User loggedInUser = HttpContext.Session.GetUser();
                wishLogic.AddToWishList(productLogic.GetProductById(id), loggedInUser);
                HttpContext.Session.UpdateUser(loggedInUser);
                return RedirectToAction("Index", "Shop");
            }
            catch (AddingWishFailedException)
            {
                return RedirectToAction("Index", "Shop");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromWishes(int id)
        {
            try
            {
                User loggedInUser = HttpContext.Session.GetUser();
                wishLogic.RemoveFromWishList(productLogic.GetProductById(id), loggedInUser);
                HttpContext.Session.UpdateUser(loggedInUser);
                return RedirectToAction("Index", "User");
            }
            catch (RemovingWishesFailedException)
            {
                return RedirectToAction("Index", "User");
            }
        }
    }
}
