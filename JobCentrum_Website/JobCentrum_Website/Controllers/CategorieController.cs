using BLL_Factory;
using BLL_interfaces;
using JobCentrum_Website.Models;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptions.Categorie;

namespace JobCentrum_Website.Controllers
{
    public class CategorieController : Controller
    {
        private readonly ICategorie_Collection _CategorieCollection ;
        private List<CategorieViewModel> categorieViews;

        public CategorieController()
        {
            _CategorieCollection = CategorieFactory.categorie_Collection();
            categorieViews = new List<CategorieViewModel>();
        }
        // GET: CategorieController
        public ActionResult Index()
        {
            CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
            categorieViews = GetAllCategories(loggedInUser);


            #region DisPlayJobs
            ////oefening
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
            //}
            #endregion

            return View(categorieViews);
        }

        // GET: CategorieController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                CategorieViewModel model = GetCategorie(id);
                return View(model);
            }
            catch
            {
                return View();
            }
            
        }

        // GET: CategorieController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategorieViewModel categorieView)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
                    _CategorieCollection.Add_Categorie(loggedInUser.Company_Id, categorieView.Categorie_name, categorieView.Categorie_description);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch(AddingCategorieFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        // GET: CategorieController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                CategorieViewModel model = GetCategorie(id);
                return View(model);
            }
            catch(FindingCategorieFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        // POST: CategorieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategorieViewModel categorieView)
        {
            try
            {
                CompanyAccountViewModel loggedInUser = HttpContext.Session.GetJobPublisher();
                ICategorie categorie = CategorieFactory.categorie(id, loggedInUser.Company_Id, categorieView.Categorie_name, categorieView.Categorie_description);
                categorie.EditCategorie(categorie.Categorie_Id, categorie.JobPublisher_id, categorie.Categorie_name, categorie.Categorie_description);
                return RedirectToAction(nameof(Index));
            }
            catch(UpdatingCategorieFailedException ex)
            {
                ViewBag.message = ex.Message;
                return View();
            }
        }

        // GET: CategorieController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                CategorieViewModel model = GetCategorie(id);
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // POST: CategorieController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategorieViewModel categorieView)
        {
            try
            {
                _CategorieCollection.Delete_Categorie(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private List<CategorieViewModel> GetAllCategories(CompanyAccountViewModel accountViewModel)
        {
            var model = _CategorieCollection.GetAllCategoriesByJobPublisher(accountViewModel.Company_Id);
            foreach (var item in model)
            {
                categorieViews.Add(ConvertLogicToViewModel(item));
            }
            return categorieViews;
        }
        private CategorieViewModel GetCategorie(int id)
        {
            ICategorie Categorie = _CategorieCollection.FindCategorie(id);
            CategorieViewModel model = ConvertLogicToViewModel(Categorie);
            return model;
        }
        private CategorieViewModel ConvertLogicToViewModel(ICategorie Categorie)
        {
            return new CategorieViewModel()
            {
                Categorie_Id = Categorie.Categorie_Id,
                Categorie_name = Categorie.Categorie_name,
                Categorie_description = Categorie.Categorie_description
            };
        }
    }
}
