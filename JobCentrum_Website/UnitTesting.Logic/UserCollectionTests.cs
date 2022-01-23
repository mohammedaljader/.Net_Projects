using BLL_Logica_laag_;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTesting.Logic
{
    [TestClass]
    public class UserCollectionTests
    {
        [TestMethod]
        public void JobSeekerLogin_WithNotCorrectData_ReturnNull()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.LoginJobSeeker("", "1234");
            //act 
            JobSeeker actual = null;
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void JobSeekerLogin_WithCorrectData_ReturnJobSeeker()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.LoginJobSeeker("Mohammed", "3311aa");
            //assert
            Assert.IsNotNull(expected);
        }
        [TestMethod]
        public void JobSeekerRegister_WithDataAndUsernameAndEmailExits_returnFalse()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.RegisterJobSeeker("test", "test4", "222", "test4@gmail.com", "333", "dd", 3, "dd");
            //assert
            Assert.IsFalse(expected);
        }
        [TestMethod]
        public void JobPublisherLogin_WithNotCorrectData_ReturnNull()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.LoginJobPublisher("", "1234");
            //act 
            JobPublisher actual = null;
            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void JobPublisherLogin_WithCorrectData_ReturnJobPublisher()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.LoginJobPublisher("testwithouthasher1", "testwithouthasher");
            //assert
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void JobPublisherRegister_WithDataAndUsernameAndEmailExits_returnFalse()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.RegisterJobPublisher("test", "Admin", "222", "Admin@r.nl", "333", "dd", "dd", "dd");
            //assert
            Assert.IsFalse(expected);
        }

        // Get all jobpublishers and check if the return value is not null
        [TestMethod]
        public void GetAllJobPublishers_returnNotNullValue()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.GetAllJobPublishers();
            //assert
            Assert.IsNotNull(expected);
        }

        // Get all jobseekers and check if the return value is not null
        [TestMethod]
        public void GetAllJobSeekers_returnNotNullValue()
        {
            //arrange 
            UserCollection userCollection = new UserCollection();
            var expected = userCollection.GetAllJobSeekers();
            //assert
            Assert.IsNotNull(expected);
        }
    }
}
