using DAL_interfaces.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_interfaces.Interfaces
{
    public interface ICategorieDAL
    {
        List<CategorieDto> GetAllCategories();
        void AddCategorie(CategorieDto categorieDto);
        void DeleteCategorie(int id);
        void EditCategorie(CategorieDto categorieDto);
        //CategorieDto FindCategorieByID(int id);
        CategorieDto FindCategorie(int id);
        List<CategorieDto> GetCategoriesByJobPublisherId(int id);
    }
}
