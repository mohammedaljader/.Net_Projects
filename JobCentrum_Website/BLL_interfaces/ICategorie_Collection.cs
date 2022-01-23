using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface ICategorie_Collection
    {
        void Add_Categorie(int user_id , string categorie_name , string categorie_description);
        void Delete_Categorie(int id);
        IReadOnlyCollection<ICategorie> GetAllCategoriesByJobPublisher(int id);
        IReadOnlyCollection<ICategorie> GetAllCategories();
        ICategorie FindCategorie(int id);
        IReadOnlyCollection<ICategorie> GetAllCategoriesWithJobs();
    }
}
