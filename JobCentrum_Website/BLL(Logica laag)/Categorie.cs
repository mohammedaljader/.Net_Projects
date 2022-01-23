using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_interfaces;
using DAL_Factory;
using DAL_interfaces.DTO_s;
using DAL_interfaces.Interfaces;

namespace BLL_Logica_laag_
{
    public class Categorie : ICategorie
    {
        #region declarations and relations
        private ICategorieDAL _categorieDAL;
        #endregion

        #region Properties
        public int Categorie_Id { get;  }
        public string Categorie_name { get;}
        public string Categorie_description { get; }
        public int JobPublisher_id { get;  }
        public List<IJob> Jobs { get; }
        #endregion

        #region Constractors
        public Categorie(string categorieName, string categorieDescription , int jobPublisher_id)
        {
            Categorie_name = categorieName;
            Categorie_description = categorieDescription;
            JobPublisher_id = jobPublisher_id;
        }
        public Categorie(int categorieId, string categorieName, string categorieDescription, int jobPublisher_id)
        {
            Categorie_Id = categorieId;
            Categorie_name = categorieName;
            Categorie_description = categorieDescription;
            JobPublisher_id = jobPublisher_id;
            _categorieDAL = CategorieDalFactory.categorieDAL();
        }
        public Categorie(CategorieDto categorieDto) 
            : this(categorieDto.Categorie_Id , categorieDto.Categorie_name , categorieDto.Categorie_description , categorieDto.Categorie_Id)
        {

        }
        public Categorie(int categorieId, string categorieName, string categorieDescription, int jobPublisher_id, List<IJob> jobs)
        {
            Categorie_Id = categorieId;
            Categorie_name = categorieName;
            Categorie_description = categorieDescription;
            JobPublisher_id = jobPublisher_id;
            Jobs = jobs;
        }
        public CategorieDto ConvertToDto()
        {
            return new CategorieDto(this.Categorie_Id, this.JobPublisher_id, this.Categorie_name, this.Categorie_description);
        }
        #endregion

        #region Methodes
        public void EditCategorie(int categorie_id, int user_id, string categorie_name, string categorie_description)
        {
            Categorie categorie = new Categorie(categorie_id , categorie_name , categorie_description , user_id);
            _categorieDAL.EditCategorie(categorie.ConvertToDto());
        }
        #endregion
    }
}
