using BLL_Factory;
using BLL_interfaces;
using JobCentrum_Website.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Job;

namespace JobCentrum_Website.Controllers
{
    public class JobController : Controller
    {
        private readonly ICategorie_Collection _CategorieCollection;
        private readonly IJobCollection _jobCollection;
        private readonly IWebHostEnvironment _HostingEnvironment;
        private List<JobViewModel> JobViews;

        public JobController(IWebHostEnvironment hostingEnvironment)
        {
            _CategorieCollection = CategorieFactory.categorie_Collection();
            _jobCollection = JobFactory.JobCollection();
            JobViews = new List<JobViewModel>();
            this._HostingEnvironment = hostingEnvironment;
        }

        // GET: JobController
        public ActionResult Index()
        {
            try
            {
                CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
                JobViews = GetAllJobsByJobPublisher(loggedInUser);
                return View(JobViews);
            }
            catch (GetAllJobsByJobPublisherFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        // GET: JobController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                JobViewModel model = GetJob(id);
                IReadOnlyCollection<ICategorie> categories = _CategorieCollection.GetAllCategories();
                ICategorie Categorie = categories.FirstOrDefault(x => x.Categorie_Id == model.Categorie_ID);
                ViewBag.categorie = Categorie;
                return View(model);
            }
            catch(FindingJobFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        // GET: JobController/Create
        public ActionResult Create()
        {

            #region DropdownList Categorie_Id
            CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
            List<CategorieViewModel> Categories = GetAllCategoriesByJobPublisher(loggedInUser.Company_Id);
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var item in Categories)
            {
                SelectListItem selectListItem = new SelectListItem
                {
                    Text = item.Categorie_name,
                    Value = item.Categorie_Id.ToString(),
                };
                selectListItems.Add(selectListItem);
            }
            ViewBag.IdCategorie = selectListItems; /*new SelectList(Categories, "Categorie_Id", "Categorie_name");*/
            #endregion

            return View();
        }

        // POST: JobController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateJobViewModel model)
        {
            try
            {
                ViewBag.Message = model.Categorie_ID;
                if (ModelState.IsValid)
                {
                    string uniqueFileName = null;
                    if (model.image != null)
                    {
                        uniqueFileName = CreateImage(model);
                    }
                    JobViewModel jobViewModel = new JobViewModel()
                    {
                        Job_name = model.Job_name,
                        Job_description = model.Job_description,
                        Job_image = uniqueFileName,
                        Job_location = model.Job_location,
                        Categorie_ID = model.Categorie_ID
                    };
                    CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
                     _jobCollection.AddJob(jobViewModel.Job_name, jobViewModel.Job_description , jobViewModel.Job_image , jobViewModel.Job_location, jobViewModel.Categorie_ID, loggedInUser.Company_Id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: JobController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                #region DropdownList Categorie_Id
                CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
                List<CategorieViewModel> Categories = GetAllCategoriesByJobPublisher(loggedInUser.Company_Id);
                List<SelectListItem> selectListItems = new List<SelectListItem>();

                foreach (var item in Categories)
                {
                    SelectListItem selectListItem = new SelectListItem
                    {
                        Text = item.Categorie_name,
                        Value = item.Categorie_Id.ToString(),
                    };
                    selectListItems.Add(selectListItem);
                }
                ViewBag.IdCategorie = selectListItems; /*new SelectList(Categories, "Categorie_Id", "Categorie_name");*/
                #endregion
                JobViewModel model = GetJob(id);
                EditJobViewModel EditJobViewModel = new EditJobViewModel()
                {
                    Job_Id = model.Job_Id,
                    Job_name = model.Job_name,
                    Job_description = model.Job_description,
                    Job_location = model.Job_location,
                    Categorie_ID = model.Categorie_ID,
                    ExistingImage = model.Job_image,
                };
                return View(EditJobViewModel);
            }
            catch(FindingJobFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditJobViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JobViewModel jobViewModel = GetJob(model.Job_Id);
                    jobViewModel.Job_name = model.Job_name;
                    jobViewModel.Job_description = model.Job_description;
                    jobViewModel.Job_location = model.Job_location;
                    jobViewModel.Categorie_ID = model.Categorie_ID;
                    CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();

                    //If the user has downloaded new Image, than the previous image get deleted en save the changes 
                    if (model.image != null)
                    {
                        if (model.ExistingImage != null)
                        {
                            string filePath = Path.Combine(_HostingEnvironment.WebRootPath,
                                                "images", model.ExistingImage);
                            System.IO.File.Delete(filePath);
                        }
                        jobViewModel.Job_image = UpdateImage(model);
                        
                        IJob _JobLogic = JobFactory.Job(jobViewModel.Job_Id, jobViewModel.Job_name, jobViewModel.Job_description, jobViewModel.Job_image, jobViewModel.Job_location, jobViewModel.Categorie_ID, loggedInUser.Company_Id);
                        _JobLogic.EditJob(jobViewModel.Job_Id, jobViewModel.Job_name, jobViewModel.Job_description, jobViewModel.Job_image, jobViewModel.Job_location, jobViewModel.Categorie_ID, loggedInUser.Company_Id);
                        return RedirectToAction(nameof(Index));
                    }
                    //if the user didn't change the image, than it will be the same image
                    else
                    {
                        jobViewModel.Job_image = model.ExistingImage;
                        IJob _JobLogic = JobFactory.Job(jobViewModel.Job_Id, jobViewModel.Job_name, jobViewModel.Job_description, jobViewModel.Job_image, jobViewModel.Job_location, jobViewModel.Categorie_ID, loggedInUser.Company_Id);
                        _JobLogic.EditJob(jobViewModel.Job_Id, jobViewModel.Job_name, jobViewModel.Job_description, jobViewModel.Job_image, jobViewModel.Job_location, jobViewModel.Categorie_ID, loggedInUser.Company_Id);
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch(UpdatingJobFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View(ex.Message);
            }
        }

        // GET: JobController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                JobViewModel model = GetJob(id);
                IReadOnlyCollection<ICategorie> categories = _CategorieCollection.GetAllCategories();
                ICategorie Categorie = categories.FirstOrDefault(x => x.Categorie_Id == model.Categorie_ID);
                ViewBag.categorie = Categorie;
                return View(model);
            }
            catch(FindingJobFailedException ex) { ViewBag.message = ex.Message; return View(); }
        }

        // POST: JobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id , JobViewModel model)
        {
            try
            {
                model = GetJob(id);
                if (model.Job_image != null)
                {
                    string filePath = Path.Combine(_HostingEnvironment.WebRootPath,
                         "images", model.Job_image);
                    System.IO.File.Delete(filePath);
                }
                _jobCollection.DeleteJob(id);
                return RedirectToAction(nameof(Index));
            }
            catch(RemovingJobFailedException ex) { ViewBag.message = ex.Message; return View(); }
        }

        private List<JobViewModel> GetAllJobsByJobPublisher(CompanyAccountViewModel accountViewModel)
        {
            var model = _jobCollection.GetAllJobsByJobPublisher(accountViewModel.Company_Id);
            foreach (var item in model)
            {
                JobViews.Add(ConvertLogicToViewModel(item));
            }
            return JobViews;
        }
        private JobViewModel GetJob(int id)
        {
            IJob job = _jobCollection.FindJob(id);
            JobViewModel model = ConvertLogicToViewModel(job);
            return model;
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
        private List<CategorieViewModel> GetAllCategoriesByJobPublisher(int id)
        {
            var model = _CategorieCollection.GetAllCategoriesByJobPublisher(id);
            List<CategorieViewModel> categorieViews = new List<CategorieViewModel>();
            foreach (var item in model)
            {
                categorieViews.Add(new CategorieViewModel
                {
                    Categorie_Id = item.Categorie_Id,
                    Categorie_name = item.Categorie_name,
                    Categorie_description = item.Categorie_description,
                });
            }
            return categorieViews;
        }
        private string CreateImage(CreateJobViewModel model)
        {
            string uniqueFileName = null;
            if (model.image != null)
            {
                string uploadsFolder = Path.Combine(_HostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.image.CopyTo(filestream);
                } 
            }
            return uniqueFileName;
        }
        private string UpdateImage(EditJobViewModel model)
        {
            string uniqueFileName = null;
            if (model.image != null)
            {
                string uploadsFolder = Path.Combine(_HostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    model.image.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }
    }
}