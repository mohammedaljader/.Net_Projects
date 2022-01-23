using BLL_interfaces;
using BLL_Logica_laag_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Factory
{
    public static class CategorieFactory
    {
        public static ICategorie_Collection categorie_Collection()
        {
            return new Categorie_Collection();
        } 
        public static ICategorie categorie(int categorie_id, int user_id, string categorie_name, string categorie_description)
        {
            return new Categorie(categorie_id, categorie_name, categorie_description, user_id);
        }
    }
}
