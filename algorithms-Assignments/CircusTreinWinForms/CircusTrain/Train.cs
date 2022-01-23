using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircusTrain
{
    public class Train
    {
        #region fields en properties
        //fields en properties
        public List<Animal> Animals { get; private set; }
        public List<Wagon> Wagons { get; private set; }
        #endregion

        #region constractor
        //constractor
        public Train()
        {
            Animals = new List<Animal>();
            Wagons = new List<Wagon>();
        }
        #endregion

        #region Methodes
        //Methodes
        public bool AddAnimalToTrain(string name, Weight weight, Diet typeAnimal)
        {
            //Create the animal
            Animal animal = new Animal(name, weight, typeAnimal);
            //Add the animal to a list of animals
            Animals.Add(animal);
            //Loop through all the wagons and check if the animal can be added.
            foreach (Wagon wagon in Wagons)
            {
                if (wagon.AddToWagon(animal))
                {
                    return true;
                }
            }
            //if the animal can't be added generate a new wagon
            if (!animal.Added)
            {
                MakeNewWagon(animal);
                return false;
            }

            return false;
        }

        //Generate a new wagon and add the animal to the wagon.
        private void MakeNewWagon(Animal animal)
        {
            Wagon wagon = new Wagon(10);
            wagon.AddToWagon(animal);
            Wagons.Add(wagon);
        }

        #endregion
    }
}
