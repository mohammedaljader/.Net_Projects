using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Review;
using ShopOnline.Models;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;

namespace ShopOnline.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IProductLogic productLogic;
        private readonly IReviewLogic reviewLogic;

        public ReviewController(IProductContext productContext, IReviewContext reviewContext)
        {
            productLogic = new ProductLogic(productContext);
            reviewLogic = new ReviewLogic(reviewContext);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(int id, ReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User loggedInUser = HttpContext.Session.GetUser();
                    Product product = new Product
                    {
                        ID = id
                    };
                    Review review = new Review
                    {
                        Message = model.Message,
                        Stars = model.Stars,
                        Product = product,
                        User = loggedInUser
                    };
                    reviewLogic.AddReview(review);
                    return RedirectToAction("Info", "Product", new { id = id });
                }
                catch (AddingReviewFailedException)
                {
                    ModelState.AddModelError("", "Adding review failed, Try again.");
                    return RedirectToAction("Info", "Product", new { id = id });
                }
            }
            return RedirectToAction("Info", "Product", new { id = id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveReview(int id)
        {
            try
            {
                Review review = new Review
                {
                    ID = id
                };
                reviewLogic.RemoveReview(review);
                return RedirectToAction("Info", "Product", new { id = id });
            }
            catch (RemovingReviewFailedException)
            {
                return RedirectToAction("Info", "Product", new { id = id });
            }
        }
    }
}
