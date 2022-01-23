using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_interfaces
{
    public interface ICategorie
    {
        public int Categorie_Id { get; }
        public string Categorie_name { get; }
        public string Categorie_description { get; }
        public int JobPublisher_id { get; }
        public List<IJob> Jobs { get; }

        void EditCategorie(int categorie_id, int user_id, string categorie_name, string categorie_description);

    }
}
