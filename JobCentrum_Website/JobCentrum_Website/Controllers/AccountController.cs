using BLL_Factory;
using BLL_interfaces;
using JobCentrum_Website.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.JobPublisher;
using Exceptions.JobSeeker;

namespace JobCentrum_Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserCollection userCollection;
        public AccountController()
        {
            userCollection = AccountFactory.UserCollection();
        }
        public IActionResult RegisterJobSeeker()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterJobSeeker(JobSeekerAccountViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool Result = userCollection.RegisterJobSeeker(obj.Fullname, obj.Username, obj.Password, obj.Email, obj.Telephone, obj.Address, obj.Experience, obj.Hobby);
                    if (Result == true)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.error = "Try again please. Username or Email exits already ";
                        return View();
                    }

                }
                return View(obj);
            }
            catch(RegisterJobSeekerFailedException ex)
            {
                ViewBag.massege = ex.Message;
                return View();
            }
        }

        public IActionResult RegisterJobPublisher()
        {
            return View();
        }
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
                        ViewBag.error = "Try again please. Username or Email exits already ";
                        return View();
                    }

                }
                return View(obj);
            }
            catch(RegisterJobPublisherFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        public IActionResult EditJobPublisher()
        {
            CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
            return View(loggedInUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobPublisher(CompanyAccountViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJobPublisher jobPublisher = AccountFactory.jobPublisherFactory(obj.Id,obj.Fullname, obj.Username, obj.Password, obj.Email, obj.Telephone, obj.Address, obj.Company_Id ,obj.Company_Name, obj.Company_Address);
                    jobPublisher.ChangeProfileJobPublisher(obj.Id, obj.Fullname, obj.Username, obj.Password, obj.Email, obj.Telephone, obj.Address, obj.Company_Id, obj.Company_Name, obj.Company_Address);
                    SignInJobPublisher(obj);
                    return RedirectToAction("Index", "Home");
                }
                return View(obj);        
            }
            catch(UpdatingJobPublisherFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        public IActionResult EditJobSeeker()
        {
            JobSeekerAccountViewModel loggedInUser = HttpContext.Session.GetJobSeeker();
            return View(loggedInUser);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJobSeeker(JobSeekerAccountViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IJobSeeker JobSeeker = AccountFactory.jobSeekerFactory(obj.Id, obj.Fullname, obj.Username, obj.Password, obj.Email, obj.Telephone, obj.Address, obj.Seeker_Id, obj.Experience, obj.Hobby);
                    JobSeeker.ChangeProfileJobSeeker(obj.Id, obj.Fullname, obj.Username, obj.Password, obj.Email, obj.Telephone, obj.Address, obj.Seeker_Id, obj.Experience, obj.Hobby);
                    SignInJobSeeker(obj);
                    return RedirectToAction("Index", "Home");
                }
                return View(obj);
            }
            catch (UpdatingJobSeekerFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }
        public IActionResult Login()
        {
            return View();
        }

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
                        if (jobPublisher != null)
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
                            return RedirectToAction("Index", "Categorie");
                        }
                        ViewBag.error = "Try again please. Username or password are not correct";
                        return View();
                    }
                    else
                    {
                        IJobSeeker JobSeeker = userCollection.LoginJobSeeker(obj.Username, obj.Password);
                        if (JobSeeker != null)
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
                        ViewBag.error = "Try again please. Username or password are not correct";
                        return View();
                    }
                }
                else
                {
                    return View(obj);
                }
            }
            catch(LoginJobPublisherFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
            catch(LoginJobSeekerFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            //Log out job publisher and jobseeker
            HttpContext.Session.ResetJobSeeker();
            HttpContext.Session.ResetJobPublisher();
            return RedirectToAction("Index", "Home");
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
