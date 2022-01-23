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
    public class AccountController : Controller
    {
        private readonly IUserCollection userCollection;

        public AccountController()
        {
            userCollection = AccountFactory.UserCollection();
        }

        // GET: AccountController/Create
        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.IsJobPublisher == true)
                    {
                        IJobPublisher jobPublisher = userCollection.LoginJobPublisher(obj.Username, obj.Password);
                        if(jobPublisher != null)
                        {
                            CompanyAccountViewModel JobPublisherModel = new CompanyAccountViewModel()
                            {
                                Id = jobPublisher.Id,
                                Fullname = jobPublisher.Fullname,
                                Username = jobPublisher.Username,
                                Password = jobPublisher.Password,
                                ConfirmPassword = jobPublisher.Password,
                                Email = jobPublisher.Email,
                                Telephone = jobPublisher.Telephone,
                                Address = jobPublisher.Address,
                                Company_Id = jobPublisher.Company_Id,
                                Company_Name = jobPublisher.Company_Name,
                                Company_Address = jobPublisher.Company_Address
                            };
                            SignInJobPublisher(JobPublisherModel);
                            return RedirectToAction("Index", "Home");
                        }
                        return View();
                    }
                    else
                    {
                        IJobSeeker JobSeeker =  userCollection.LoginJobSeeker(obj.Username, obj.Password);
                        if(JobSeeker != null) 
                        {
                            JobSeekerAccountViewModel JobSeekerModel = new JobSeekerAccountViewModel()
                            {
                                Id = JobSeeker.Id,
                                Fullname = JobSeeker.Fullname,
                                Username = JobSeeker.Username,
                                Password = JobSeeker.Password,
                                ConfirmPassword = JobSeeker.Password,
                                Email = JobSeeker.Email,
                                Telephone = JobSeeker.Telephone,
                                Address = JobSeeker.Address,
                                Seeker_Id = JobSeeker.Seeker_Id,
                                Experience = JobSeeker.Experience,
                                Hobby = JobSeeker.Hobby
                            };
                            SignInJobSeeker(JobSeekerModel);
                            return RedirectToAction("Index", "Home");
                        }
                        return View();
                    }
                }
                else
                {
                    return View(obj);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult RegisterJobSeeker()
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterJobSeeker(JobSeekerAccountViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool Result =  userCollection.RegisterJobSeeker(obj.Fullname, obj.Username, obj.Password, obj.Email, obj.Telephone, obj.Address, obj.Experience, obj.Hobby);
                    if(Result == true)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View();
                    }
                    
                }
                return View(obj);
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult RegisterJobPublisher()
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterJobPublisher(CompanyAccountViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool Result = userCollection.RegisterJobPublisher(obj.Fullname, obj.Username, obj.Password, obj.Email, obj.Telephone, obj.Address, obj.Company_Name, obj.Company_Address);
                    if (Result == true)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        return View();
                    }

                }
                return View(obj);
            }
            catch
            {
                return View();
            }
        }

        private void SignInJobSeeker(JobSeekerAccountViewModel loggedInUser)
        {
            HttpContext.Session.UpdateJobSeeker(loggedInUser);
        }
        private void SignInJobPublisher(CompanyAccountViewModel loggedInUser)
        {
            HttpContext.Session.UpdateJobPublisher(loggedInUser);
        }
    }
}
