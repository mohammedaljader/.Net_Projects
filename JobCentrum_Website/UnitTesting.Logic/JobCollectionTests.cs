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
    public class JobCollectionTests
    {
        [TestMethod]
        public void GetAllJobs__ReturnNotNull()
        {
            //arrange 
            JobCollection jobCollection = new JobCollection();
            var expected = jobCollection.GetAllJobs();
            //assert
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void GetAllJobsByJobPublisher_WithGoodId__ReturnNotNull()
        {
            //arrange 
            JobCollection jobCollection = new JobCollection();
            var expected = jobCollection.GetAllJobsByJobPublisher(2); //id of jobpublisher
            //assert
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void FindJob_WithGoodId__ReturnNotNull()
        {
            //arrange 
            JobCollection jobCollection = new JobCollection();
            var expected = jobCollection.FindJob(2); //id of jobpublisher
            //assert
            Assert.IsNotNull(expected);
        }

        [TestMethod]
        public void DeletingJob_WithBadId__ReturnNull()
        {
            //arrange 
            JobCollection jobCollection = new JobCollection();
            var job = jobCollection.Jobs.FirstOrDefault(x => x.Job_Id == 1111);
            //assert
            Assert.IsNull(job);
        }

        [TestMethod]
        public void DeletingJob_WithGoodId__ReturnNotNull()
        {
            //arrange 
            JobCollection jobCollection = new JobCollection();
            var job = jobCollection.Jobs.FirstOrDefault(x => x.Job_Id == 0);
            //assert
            Assert.IsNotNull(job);
        }

        [TestMethod]
        public void AddingJob_WithGoodData()
        {
            //arrange 
            JobCollection jobCollection = new JobCollection();
            jobCollection.Jobs.Add(new Job(1, "test", "test", "test", "dd" , 111, 111));
            var job = jobCollection.Jobs.FirstOrDefault(x => x.Job_Id == 1);
            //assert
            Assert.IsNotNull(job);
        }
    }
}
