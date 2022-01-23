using BLL_Factory;
using BLL_interfaces;
using JobCentrum_Website.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Job;
using System.IO;
using Exceptions.JobApplication;
using Exceptions.Categorie;

namespace JobCentrum_Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _HostingEnvironment;
        private readonly IJobCollection _jobCollection;
        private readonly ICategorie_Collection _CategorieCollection;
        private readonly IJobSeeker _Application;
        private List<JobViewModel> JobViews;


        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _HostingEnvironment = hostingEnvironment;
            _jobCollection = JobFactory.JobCollection();
            _CategorieCollection = CategorieFactory.categorie_Collection();
            _Application = ApplicationFactory.Application();
            JobViews = new List<JobViewModel>();
        }

        public IActionResult Index()
        {
            HttpContext.Session.CreateJobSeeker();
            HttpContext.Session.CreateJobPublisher();

            #region DisPlayJobs
            //oefening
            //var categories = _CategorieCollection.GetAllCategoriesWithJobs();
            //foreach (var item in categories)
            //{
            //    List<JobViewModel> jobViews = new List<JobViewModel>();
            //    foreach (var job in item.Jobs)
            //    {
            //        jobViews.Add(new JobViewModel()
            //        {
            //            Job_Id = job.Job_Id,
            //            Job_description = job.Job_description,
            //            Job_name = job.Job_name,
            //            Job_image = job.Job_image,
            //            Job_location = job.Job_location
            //        });
            //    }
            //    CategorieViewModel categorieViewModel = new CategorieViewModel()
            //    {
            //        Categorie_Id = item.Categorie_Id,
            //        Categorie_name = item.Categorie_name,
            //        Categorie_description = item.Categorie_description,
            //        Jobs = jobViews
            //    };
            //    categorieViews.Add(categorieViewModel);
            //}
            #endregion

            try
            {
                JobViews = GetAllJobs();

                return View(JobViews);
            }
            catch (GetAllJobsFailedException ex)
            {
                ViewBag.massage = ex.Message;
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                JobViews = GetAllJobs();
                var model = JobViews.FirstOrDefault(x => x.Job_Id == id);
                IReadOnlyCollection<ICategorie> categories = _CategorieCollection.GetAllCategories();
                ICategorie Categorie = categories.FirstOrDefault(x => x.Categorie_Id == model.Categorie_ID);
                ViewBag.categorie = Categorie;
                if (model != null)
                {
                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (FindingJobFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
            catch (GetAllCategoriesFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        public ActionResult Apply(int id)
        {
            JobSeekerAccountViewModel JobSeeker = HttpContext.Session.GetJobSeeker();
            //Only JobSeeker can apply
            if (JobSeeker.Seeker_Id == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Apply(ApplicationViewModel applicationView, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (applicationView.CV != null)
                    {
                        uniqueFileName = SaveCV(applicationView);
                    }
                    JobSeekerAccountViewModel actieveUser = HttpContext.Session.GetJobSeeker();
                    // id == Job_Id
                    _Application.ApplyForJob(uniqueFileName, applicationView.Motivation, id, actieveUser.Seeker_Id);
                    return RedirectToAction("Index", "Home");
                }
                 return View();
            }
            catch(AddingApplicationFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }
        //DeleteApply
        public ActionResult DeleteApply(int id)
        {
            try
            {
                JobSeekerAccountViewModel JobSeeker = HttpContext.Session.GetJobSeeker();
                List<ApplicationViewModel> jobViews = GetAllAppliedJobByJobSeeker(JobSeeker.Seeker_Id);
                var Application = jobViews.FirstOrDefault(x => x.Apply_Id == id);
                return View(Application);
            }
            catch (GetingAllApplicationsByJobSeekerFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteApply(ApplicationViewModel applicationView, int id)
        {
            try
            {
                JobSeekerAccountViewModel JobSeeker = HttpContext.Session.GetJobSeeker();
                List<ApplicationViewModel> jobViews = GetAllAppliedJobByJobSeeker(JobSeeker.Seeker_Id);
                applicationView = jobViews.FirstOrDefault(x => x.Apply_Id == id);
                if (applicationView.CV_Path != null)
                {
                    string filePath = Path.Combine(_HostingEnvironment.WebRootPath,
                         "CV", applicationView.CV_Path);
                    System.IO.File.Delete(filePath);
                }
                _Application.RemoveApplication(id);
                return RedirectToAction("Applications", "Home");
            }
            catch(RemovingApplicationFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }
        public ActionResult Applications()
        {
            try
            {
                JobSeekerAccountViewModel JobSeeker = HttpContext.Session.GetJobSeeker();
                List<ApplicationViewModel> jobViews = GetAllAppliedJobByJobSeeker(JobSeeker.Seeker_Id);
                return View(jobViews);
            }
            catch (GetingAllApplicationsByJobSeekerFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ChoiceAccount()
        {
            return View();
        }

        private JobViewModel ConvertLogicToViewModel(IJob job)
        {
            return new JobViewModel()
            {
                Job_Id = job.Job_Id,
                Job_name = job.Job_name,
                Job_description = job.Job_description,
                Job_image = job.Job_image,
                Job_location = job.Job_location,
                Categorie_ID = job.Categorie_ID
            };
        }
        private List<JobViewModel> GetAllJobs()
        {
            var model = _jobCollection.GetAllJobs();
            foreach (var item in model)
            {
                JobViews.Add(ConvertLogicToViewModel(item));
            }
            return JobViews;
        }

        private JobViewModel GetJob(int Job_Id)
        {
            List<JobViewModel> Jobs = GetAllJobs();
            JobViewModel jobView = Jobs.FirstOrDefault(x => x.Job_Id == Job_Id);
            return jobView;
        }

        private List<ApplicationViewModel> GetAllAppliedJobByJobSeeker(int id)
        {
            var model = _Application.GetAllApplicationsByJobSeeker(id);
            List<ApplicationViewModel> jobViews = new List<ApplicationViewModel>();
            foreach (var item in model)
            {
                jobViews.Add(new ApplicationViewModel() { 
                    Apply_Id = item.Apply_Id,
                    Motivation = item.Motivation,
                    Job = GetJob(item.Job_id),
                    CV_Path = item.CV
                });
            }
            return jobViews;
        }

        private string SaveCV(ApplicationViewModel model)
        {
            string uniqueFileName = null;
            if (model.CV != null)
            {
                string uploadsFolder = Path.Combine(_HostingEnvironment.WebRootPath, "CV");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CV.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.CV.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }

    }
}
