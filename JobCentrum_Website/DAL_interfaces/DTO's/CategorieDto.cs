using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.DTO_s
{
    public class CategorieDto
    {
        public int Categorie_Id { get; set; }
        public int JobPublisher_id { get; set; }
        public string Categorie_name { get; set; }
        public string Categorie_description { get; set; }

        //public CategorieDto(int categorieId, CompanyDto companyDto, string categorieName, string categorieDescription)
        //{
        //    Categorie_Id = categorieId;
        //    Categorie_name = categorieName;
        //    Categorie_description = categorieDescription;
        //    JobPublisher = companyDto;
        //}

        public CategorieDto(int categorieId, int companyDto_id, string categorieName, string categorieDescription)
        {
            Categorie_Id = categorieId;
            Categorie_name = categorieName;
            Categorie_description = categorieDescription;
            JobPublisher_id = companyDto_id;
        }
        public CategorieDto()
        {
           
        }
    }
}
