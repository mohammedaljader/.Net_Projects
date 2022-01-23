using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circustrein
{
    public class Animal
    {
        //properties
        public string Name { get; set; }
        public Weight Weight { get; set; }
        public Diet Diet { get; set; }

        //Constractor
        public Animal(string name , Weight weight , Diet diet)
        {
            this.Name = name;
            this.Weight = Weight;
            this.Diet = diet;
        }

        //Override Tostring 
        public override string ToString()
        {
            return $"Name: {this.Name}, weight:{this.Weight} & Diet: {this.Diet}";
        }
    }
}
