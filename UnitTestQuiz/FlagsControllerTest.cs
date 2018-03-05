using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiz_V01.Controllers;

namespace UnitTestQuiz
{
    [TestClass]
    public class FlagsControllerTest
    {
        [TestMethod]
        public void FlagsCollection_ShouldBeCreated()
        {
            var controller = new FlagsController();
            var result = controller.GetAllFlags();
            
            Assert.IsTrue(result.Count == 10);
        }

        public void IfFirstFlag_ShouldPass()
        {
            var controller = new FlagsController();
            var result = controller.GetAllFlags();

            var firstFlag = controller.GetFirstFlag(result);

            Assert.IsTrue(firstFlag.FlagName == result[0].FlagName);
        }

        public void IfNotFirstFlag_ShouldNotPass()
        {
            var controller = new FlagsController();
            var result = controller.GetAllFlags();

            var firstFlag = controller.GetFirstFlag(result);

            Assert.IsTrue(firstFlag.FlagName == result[4].FlagName);
        }

        public void IfCollectionShuffled_ShouldPass()
        {
            var controller = new FlagsController();
            var standardCollection = controller.GetAllFlags();
            var shuffledCollection = controller.ShuffleCollection(standardCollection);

            Assert.IsTrue(standardCollection[0].FlagName != shuffledCollection[0].FlagName);
        }

        public void IfShuffleStrings_ShouldPadd()
        {
            var  test = new List<string> {"first", "second", "third"};
            var controller = new FlagsController();

            var shuffledStrings = controller.ShuffleStrings(test);

            Assert.IsTrue(test[0] != shuffledStrings[0]);
        }

        public void IfListContainsCurrentFlag_ShouldPass()
        {
            var controller = new FlagsController();
            var flagsCollection = controller.GetAllFlags();
            var shuffledCollection = controller.ShuffleCollection(flagsCollection);
            var currentFlag = controller.GetCurrentFlag(shuffledCollection);
            var countriesList = controller.GetCountriesForQuiz(shuffledCollection);

            Assert.IsTrue(countriesList.Contains(currentFlag.FlagName));
        }

        public void IfCollectionNotContainsCurrentFlag_ShouldPass()
        {
            var controller = new FlagsController();
            var flagsCollection = controller.GetAllFlags();
            var shuffledCollection = controller.ShuffleCollection(flagsCollection);
            var currentFlag = controller.GetCurrentFlag(shuffledCollection);

            var quizCollection = controller.CollectionWithoutCurrentFlag(shuffledCollection);

            Debug.WriteLine("Current flasg {0}", currentFlag.FlagName);

            Assert.AreNotEqual(quizCollection.SelectMany(x=>x.FlagName), currentFlag.FlagName);
        }
    }
}
