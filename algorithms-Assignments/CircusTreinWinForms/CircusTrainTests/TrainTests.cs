using CircusTrain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircusTrainTests
{
    [TestClass]
    public class TrainTests
    {
        [TestMethod]
        public void AddAnimal_NoWagonGenerated_returnFalse()
        {
            //arrange
            //No wagon was created which invokes the NoWagonFound which if executed correctly will add a new wagon to train.Wagons
            //and will also add 
            Train train = new Train();
            //act 
            bool actual = train.AddAnimalToTrain("test", Weight.Large, Diet.Carnivore);
            //assert
            Assert.IsFalse(actual);
            Assert.AreEqual(1, train.Wagons.Count);
            Assert.AreEqual(1, train.Wagons[0].AnimalsInWagon.Count);
        }

        [TestMethod]
        public void AddAnimal_WagonIsFull_returnFalse()
        {
            //arrange
            //THe animal can't be added to a existing wagon which will invoke the NoWagonFound method which will generate a new wagon and add the animal to said wagon
            //which means the train.wagons should contain 2 wagons.
            Train train = new Train();
            Wagon wagon = new Wagon(3);
            train.Wagons.Add(wagon);
            //act
            bool actual = train.AddAnimalToTrain("test", Weight.Large, Diet.Carnivore);
            //assert
            Assert.IsFalse(actual);
            Assert.AreEqual(2, train.Wagons.Count);
            Assert.AreEqual(1, train.Wagons[1].AnimalsInWagon.Count);
        }

        [TestMethod()]
        public void AddAnimal_WagonIsGanerated_returnTrueTest()
        {
            //arrange
            Train train = new Train();
            Wagon wagon = new Wagon(10);
            train.Wagons.Add(wagon);
            //act
            bool actual = train.AddAnimalToTrain("test", Weight.Medium, Diet.Herbivore); ;
            //assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void AddAnimal_WagonIsGanerated_returnFalseTest()
        {
            //arrange
            Train train = new Train();
            Wagon wagon = new Wagon(10);
            train.Wagons.Add(wagon);
            train.AddAnimalToTrain("test", Weight.Medium, Diet.Herbivore);
            //act
            bool actual = train.AddAnimalToTrain("test", Weight.Medium, Diet.Carnivore);
            //assert
            Assert.IsFalse(actual);
        }
    }
}
