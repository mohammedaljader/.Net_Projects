using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL_Logica_laag_;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting.Logic
{
    [TestClass]
    public class CategorieCollectionTests
    {
        [TestMethod]
        public void GetAllCategories__ReturnNotNull()
        {
            //arrange 
            Categorie_Collection categorie_Collection = new Categorie_Collection();
            var expected = categorie_Collection.GetAllCategories();
            //assert
            Assert.IsNotNull(expected);
        }
        [TestMethod]
        public void GetAllCategoriesWithTheListOfJobs__ReturnNotNull()
        {
            //arrange 
            Categorie_Collection categorie_Collection = new Categorie_Collection();
            var expected = categorie_Collection.GetAllCategoriesWithJobs();
            //assert
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void GetAllCategoriesByJobPublisher_WithGoodId__ReturnNotNull()
        {
            //arrange 
            Categorie_Collection categorie_Collection = new Categorie_Collection();
            var expected = categorie_Collection.GetAllCategoriesByJobPublisher(2); //id of jobpublisher
            //assert
            Assert.IsNotNull(expected);
        }
        [TestMethod]
        public void FindCategorie_WithGoodId__ReturnNotNull()
        {
            //arrange 
            Categorie_Collection categorie_Collection = new Categorie_Collection();
            var expected = categorie_Collection.FindCategorie(2); //id of jobpublisher
            //assert
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void DeletingCategorie_WithBadId__ReturnNull()
        {
            //arrange 
            Categorie_Collection categorie_Collection = new Categorie_Collection();
            var categorie = categorie_Collection.categories.FirstOrDefault(x => x.Categorie_Id == 111111);    
            //assert
            Assert.IsNull(categorie);
        }

        [TestMethod]
        public void DeletingCategorie_WithGoodId__ReturnNotNull()
        {
            //arrange 
            Categorie_Collection categorie_Collection = new Categorie_Collection();
            var categorie = categorie_Collection.categories.FirstOrDefault(x => x.Categorie_Id == 0);
            //assert
            Assert.IsNotNull(categorie);
        }

        [TestMethod]
        public void AddingCategorie_WithGoodData()
        {
            //arrange 
            Categorie_Collection categorie_Collection = new Categorie_Collection();
            categorie_Collection.categories.Add(new Categorie(1, "test", "test", 11));
            var categorie = categorie_Collection.categories.FirstOrDefault(x => x.Categorie_Id == 0);
            //assert
            Assert.IsNotNull(categorie);
        }
    }
}
