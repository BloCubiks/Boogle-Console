using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Projet_A2_S1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] result = Dictionnaire.Fusion(["4", "5", "1"], ["5", "3"]);
            Assert.AreEqual(["4", "5", "1", "5", "3"], result);
        }
        [TestMethod]
        public void TestMethod2()
        {
            string[] result = Dictionnaire.TriFusion(["7", "3", "1", "5"]);
            Assert.AreEqual(["1", "3", "5", "7"], result);
        }
        [TestMethod]
        public void TestMethod3()
        {
            string[] result = Dictionnaire.TriBogo(["5", "1", "8"]);
            Assert.AreEqual(["1", "5", "8"], result);
        }
        [TestMethod]
        public void TestMethod4()
        {
            string[] result = Dictionnaire.TriInsertion(["4", "7", "1", "9"]);
            Assert.AreEqual(["1", "4", "7", "9"], result);
        }
        [TestMethod]
        public void TestMethod5()
        {
            bool result = Dictionnaire.RechDicoRecursif("ami", 0, Dictionnaire.GetDictionnaire.Length);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void TestMethod6()
        {
            bool result = Plateau.MotPresent("ami");
            Assert.IsTrue(result);
        }
    }
}
