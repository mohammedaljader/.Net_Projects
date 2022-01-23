using Microsoft.VisualStudio.TestTools.UnitTesting;
using CircusTrain;
using System.Linq;

namespace CircusTrainTests
{
    [TestClass]
    public class WagonTests
    {
        [TestMethod]
        public void AddNoneToWagonTest()
        {
            //arrange
            Wagon wagon = new Wagon(10);
            int expected = 0;
            //act
            int actual = wagon.AnimalsInWagon.Count();
            //assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void CheckWeight_WeightOfAnimalIsLessThanWeightOfWagon_ReturnTrueTest1()
        {
            // arrange
            Wagon wagon = new Wagon(10);
            Animal animal = new Animal("test", Weight.Large, Diet.Herbivore);
            //act
            var actual = wagon.CheckWeight(animal);
            //assert
            Assert.IsTrue(actual,"");
            
        }

        [TestMethod]
        public void CheckWeight_WeightOfAnimalIsLessThanWeightOfWagon_ReturnTrueTest2()
        {
            //arrange
            //By leaving the wagon empty the compatibility test will automatically pass so this will only focus on the weight test
            Wagon wagon = new Wagon(6);
            Animal animal = new Animal("test", Weight.Large, Diet.Herbivore);
            //act
            bool actual = wagon.AddToWagon(animal);
            //assert
            Assert.IsTrue(actual);
        }
        [TestMethod]
        public void CheckWeight_CanNotBeAddedToWagonBecauseOfALargeWeight_ReturnFalse()
        {
            //arrange
            //By leaving the wagon empty the compatibility test will automatically pass so this will only focus on the weight test
            Wagon wagon = new Wagon(4); // The maxWeight of wagon is less than the weight of animal
            Animal animal = new Animal("test", Weight.Large, Diet.Herbivore);
            //act
            bool actual = wagon.AddToWagon(animal);
            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckCompatibility_CheckingOfTheAnimalCanBeAddedToWagonWithOtherAnimals_returnTrueTest1()
        {
            //arrange
            //By giving the wagon a huge capacity we can safely bypass the weight test and will only have to pass the compatibility test.
            //This test will check if we can add a larger herbivore to a wagon with a smaller carnivore.
            Wagon wagon = new Wagon(100);
            Animal animal = new Animal("test", Weight.Medium, Diet.Carnivore);
            Animal animal2 = new Animal("test", Weight.Large, Diet.Herbivore);
            wagon.AddToWagon(animal);
            //act
            bool actual = wagon.AddToWagon(animal2);
            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckCompatibility_CheckingOfTheAnimalCanBeAddedToWagonWithOtherAnimals_returnTrueTest2()
        {
            //arrange
            //By giving the wagon a huge capacity we can safely bypass the weight test and will only have to pass the compatibility test.
            //This test will check if we can add a smaller carnivore to a wagon with a larger herbivore.
            Wagon wagon = new Wagon(100);
            Animal animal = new Animal("test", Weight.Large, Diet.Herbivore);
            Animal animal2 = new Animal("test", Weight.Medium, Diet.Carnivore);
            wagon.AddToWagon(animal);
            //act
            bool actual = wagon.AddToWagon(animal2);
            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckCompatibility_CheckingOfTheAnimalCanBeAddedToWagonWithOtherAnimals_returnFalseTest1()
        {
            //arrange
            //By giving the wagon a huge capacity we can safely bypass the weight test and will only have to pass the compatibility test.
            //This test will check if the method prevents a smaller herbivore being added to wagon with a larger carnivore.
            Wagon wagon = new Wagon(100);
            Animal animal = new Animal("test", Weight.Medium, Diet.Carnivore);
            Animal animal2 = new Animal("test", Weight.Medium, Diet.Herbivore);
            wagon.AddToWagon(animal);
            //act
            bool actual = wagon.AddToWagon(animal2);
            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckCompatibility_CheckingOfTheAnimalCanBeAddedToWagonWithOtherAnimals_returnFalseTest2()
        {
            //arrange
            //By giving the wagon a huge capacity we can safely bypass the weight test and will only have to pass the compatibility test.
            //This test will check if the method prevents a carnivore and herbivore of the same size being added to the same wagon
            Wagon wagon = new Wagon(100);
            Animal animal = new Animal("test", Weight.Large, Diet.Carnivore);
            Animal animal2 = new Animal("test", Weight.Large, Diet.Herbivore);
            wagon.AddToWagon(animal);
            //act
            bool actual = wagon.AddToWagon(animal2);
            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckCompatibility_CheckingOfTheAnimalCanBeAddedToWagonWithOtherAnimals_returnFalseTest3()
        {
            //arrange
            //By giving the wagon a huge capacity we can safely bypass the weight test and will only have to pass the compatibility test.
            //This test will check if the method prevents a smaller herbivore being added to wagon with a larger carnivore.
            Wagon wagon = new Wagon(100);
            Animal animal = new Animal("test", Weight.Small, Diet.Carnivore);
            Animal animal2 = new Animal("test", Weight.Small, Diet.Herbivore);
            wagon.AddToWagon(animal);
            //act
            bool actual = wagon.AddToWagon(animal2);
            //assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CheckCompatibility_CheckingOfTheAnimalCanBeAddedToWagonWithOtherAnimals_returnFalseTest4()
        {
            //arrange
            //By giving the wagon a huge capacity we can safely bypass the weight test and will only have to pass the compatibility test.
            //This test will check if the method prevents a smaller herbivore being added to wagon with a larger carnivore.
            Wagon wagon = new Wagon(100);
            Animal animal = new Animal("test", Weight.Small, Diet.Carnivore);
            Animal animal2 = new Animal("test", Weight.Small, Diet.Carnivore);
            wagon.AddToWagon(animal);
            //act
            bool actual = wagon.AddToWagon(animal2);
            //assert
            Assert.IsFalse(actual);
        }
    }
}
