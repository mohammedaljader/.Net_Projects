using Exceptions.User;
using ShopOnline;
using ShopOnline.Models;
using Interfaces.Contexts;
using Interfaces.Logic;
using Logic.LogicObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DataModels;
using System;

namespace ShopOnline.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLogic userLogic;
        private readonly IWishLogic wishLogic;
        private readonly IOrderLogic orderLogic;

        public UserController(IUserContext userContext, IWishContext wishContext, IOrderContext orderContext)
        {
            userLogic = new UserLogic(userContext);
            wishLogic = new WishLogic(wishContext);
            orderLogic = new OrderLogic(orderContext);
        }

        public IActionResult Index()
        {
            User loggedInUser = HttpContext.Session.GetUser();
            ViewBag.User = loggedInUser;
            ViewBag.Orders = orderLogic.GetAllOrdersByUser(loggedInUser);
            ViewBag.Wishes = wishLogic.GetWishesByUser(loggedInUser);
            return String.IsNullOrEmpty(loggedInUser.Name) ? (IActionResult)RedirectToAction("SignIn", "User") : View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User loggedInUser = userLogic.Login(user.Email, user.Password);
                    loggedInUser.WishList = wishLogic.GetWishesByUser(loggedInUser);
                    SignInUser(loggedInUser);
                    return RedirectToAction("Index", "User");
                }
                catch (AuthenticationFailedException)
                {
                    ModelState.AddModelError("", "The email or password provided is incorrect.");
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User loggedInUser = UpdateUserInfo(user);
                    userLogic.EditUser(loggedInUser);
                    HttpContext.Session.ResetUser();
                    return RedirectToAction("Index", "Home");
                }
                catch (UpdatingUserFailedException)
                {
                    ModelState.AddModelError("", "Updating user information failed");
                    return RedirectToAction("Index", "User");
                }
            }
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userLogic.Register(new User
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password
                    });
                    return RedirectToAction("SignIn", "User");
                }
                catch (RegistrationFailedException)
                {
                    ModelState.AddModelError("", "Registration failed, Try again");
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Session.ResetUser();
            return RedirectToAction("Index", "Home");
        }

        #region Methods
        private void SignInUser(User loggedInUser)
        {
            HttpContext.Session.UpdateUser(loggedInUser);
        }

        private User UpdateUserInfo(RegisterViewModel user)
        {
            User loggedInUser = HttpContext.Session.GetUser();
            loggedInUser.Email = user.Email;
            loggedInUser.Name = user.Name;
            loggedInUser.Password = user.Password;
            return loggedInUser;
        }
        #endregion
    }
}
