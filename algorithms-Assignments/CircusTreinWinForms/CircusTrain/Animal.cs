using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircusTrain
{
    public class Animal
    {
        #region Properties
        //properties
        public string Name { get; set; }
        public Weight Weight { get; set; }
        public Diet Diet { get; set; }
        public bool Added { get; set; } = false;
        #endregion

        #region Constractor
        //Constractor
        public Animal(string name, Weight weight, Diet diet)
        {
            this.Name = name;
            this.Weight = weight;
            this.Diet = diet;
        }
        #endregion

        #region Methodes
        //Override Tostring 
        public override string ToString()
        {
            return $"Name: {this.Name}, weight:{this.Weight} & Diet: {this.Diet}";
        }
        #endregion
    }
}
