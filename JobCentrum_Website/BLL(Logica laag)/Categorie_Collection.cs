using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_Factory;
using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;
using Exceptions.Categorie;

namespace BLL_Logica_laag_
{
    public class Categorie_Collection : ICategorie_Collection
    {
        #region declarations and relations
        private readonly ICategorieDAL _categorieDAL;
        private readonly IUser_companyDAL _userCollection;
        private readonly IJobDAL _jobDAL;
        public List<Categorie> categories;
        #endregion

        #region Constractors
        public Categorie_Collection()
        {
            _categorieDAL = CategorieDalFactory.categorieDAL();
            _userCollection = AccountDalFactory.User_JobPublisher();
            _jobDAL = JobDalFactory.JobDAL();
            categories = new List<Categorie>();
            categories.Add(new Categorie( "test", "test" , 0));
            #region retrieve data from the database 
            foreach (CategorieDto categorieDto in _categorieDAL.GetAllCategories())
            {
                Categorie categorie = new Categorie(categorieDto);
                categories.Add(categorie);
            }
            #endregion
        }
        #endregion

        #region Methodes
        public void Add_Categorie(int user_id, string categorie_name, string categorie_description)
        {
            CompanyDto JobPublisherDto =  _userCollection.FindJobPublisher(user_id);
            if(JobPublisherDto != null)
            {
               // JobPublisher jobPublisher = new JobPublisher(dto.Id, dto.Fullname, dto.Username, dto.Password, dto.Email, dto.Telephone, dto.Address, dto.Company_Id, dto.Company_Name, dto.Company_Address);
                Categorie categorie = new Categorie(categorie_name, categorie_description, user_id);
                categories.Add(categorie);
                _categorieDAL.AddCategorie(categorie.ConvertToDto());
            }
            
        }
        public void Delete_Categorie(int id)
        {
            Categorie categorie = categories.FirstOrDefault(x => x.Categorie_Id == id);
            if (categorie != null)
            {
                categories.Remove(categorie);
                _categorieDAL.DeleteCategorie(id);
            }
            else
            {
                throw new FindingCategorieFailedException("Unable to find the categorie. The object or the id is not exits!!!");
            }
        }
        public ICategorie FindCategorie(int id)
        {
            Categorie categorie = categories.FirstOrDefault(x => x.Categorie_Id == id);
            if (categorie != null)
            {
                return categorie;
            }
            else
            {
                throw new FindingCategorieFailedException("Unable to find the categorie. The object or the id is not exits!!!");
            }
        }
        public IReadOnlyCollection<ICategorie> GetAllCategories()
        {
            List<CategorieDto> categorieDtos = _categorieDAL.GetAllCategories();
            categories.Clear();
            foreach (var dto in categorieDtos)
            {
                categories.Add(new Categorie(dto));
            }
            return categories.AsReadOnly();
        }
        //JobPublisher ID
        public IReadOnlyCollection<ICategorie> GetAllCategoriesByJobPublisher(int id)
        {
            List<CategorieDto> categorieDtos = _categorieDAL.GetCategoriesByJobPublisherId(id);
            if(categorieDtos == null)
            {
                throw new GetAllCategoriesByJobPublisherFailedException("Unable to get all categories because id is not exits or the object is not exits!! Please try again");
            }
            categories.Clear();
            foreach (var dto in categorieDtos)
            {
                categories.Add(new Categorie(dto));
            }
            return categories.AsReadOnly();
        }
        public IReadOnlyCollection<ICategorie> GetAllCategoriesWithJobs()
        {
            List<CategorieDto> categories = _categorieDAL.GetAllCategories();
            List<ICategorie> categoriesWithJobs = new List<ICategorie>();
            foreach (var item in categories)
            {
                List<JobDto> jobsDto = _jobDAL.GetAllJobsByCategorieId(item.Categorie_Id);
                List<IJob> jobs = new List<IJob>();
                foreach (var dto in jobsDto)
                {
                    jobs.Add(new Job(dto));
                }
                categoriesWithJobs.Add(new Categorie(item.Categorie_Id, item.Categorie_name, item.Categorie_description, item.JobPublisher_id, jobs));
            }
            return categoriesWithJobs.AsReadOnly();
        }
        #endregion
    }
}
