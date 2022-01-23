using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircusTrain
{
    public class Wagon
    {

        #region Fields and properties 
        //Fields and properties 
        public int MaxWeight { get; private set; } = 10;
        public List<Animal> AnimalsInWagon { get; private set; }
        #endregion

        #region Constractor
        //Constractor
        public Wagon(int weightOfWagons)
        {
            AnimalsInWagon = new List<Animal>();
            MaxWeight = weightOfWagons;
        }
        #endregion

        #region Methodes 
        //checks if an animal can be added to a wagon, and adds to animals list in wagon if possible.
        public bool AddToWagon(Animal animal)
        {
            //The animals passes the tests and gets added
            if (CheckCompatibility(animal) && CheckWeight(animal))
            {
                //Animal gets marked as added and the weight of the wagon gets adjusted.
                AnimalsInWagon.Add(animal);
                animal.Added = true;
                MaxWeight = MaxWeight - (int)animal.Weight;
                return true;
            }
            //The animal can't be added
            else
            {
                return false;
            }
        }

        //Checks if the weight of the animal is compatible with the leftover space in the wagon
        private bool CheckWeight(Animal animal)
        {
            //The animal fits and gets added to the wagon
            if (this.MaxWeight - animal.Weight >= 0)
            {
                return true;
            }
            //The animal can't be added
            else
            {
                return false;
            }
        }

        //Checks for conficts between herbivores and carnivores
        private bool CheckCompatibility(Animal animal)
        {
            #region LINQ WAY
            //List<Animal> carnivoresInWagon = this.AnimalsInWagon.FindAll(a => a.Diet == Diet.Carnivore);
            //if (animal.Diet == Diet.Carnivore)
            //{
            //    carnivoresInWagon.Add(animal);   
            //}
            //if (carnivoresInWagon.Any(c => animal.Weight <= c.Weight || animal.Weight >= c.Weight))
            //{
            //    return false;
            //}
            #endregion

            #region ForEach Way
            foreach (Animal animalToCheck in this.AnimalsInWagon)
            {
                //If there are any conflicts return false
                if (animalToCheck.Weight <= animal.Weight && animal.Diet == Diet.Carnivore || animalToCheck.Diet == Diet.Carnivore && animalToCheck.Weight >= animal.Weight)
                {
                    return false;
                }
            }
            #endregion

            //No conflicts return true
            return true;
        }

        public override string ToString()
        {
            return $"Wagon, {MaxWeight}";
        }
        #endregion
    }
}
