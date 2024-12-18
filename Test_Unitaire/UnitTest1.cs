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
            string[] tab1 = { "4", "5", "1" };
            string[] tab2 = { "5", "3" };
            string[] tab3 = { "4", "5", "3", "5", "1" };
            string[] result = Dictionnaire.Fusion(tab1,tab2);
            for (int i = 0; i < tab3.Length; i++)
            {
                Assert.AreEqual(tab3[i], result[i]);
            }
        }
        [TestMethod]
        public void TestMethod2()
        {
            string[] tab1 = { "7", "3", "1", "5" };
            string[] tab2 = { "1", "3", "5", "7" };
            string[] result = Dictionnaire.TriFusion(tab1);
            for (int i = 0; i < tab2.Length; i++)
            {
                Assert.AreEqual(tab2[i], result[i]);
            }
        }
        [TestMethod]
        public void TestMethod3()
        {
            string[] tab1 = { "5", "1", "8" };
            string[] tab2 = { "1", "5", "8" };
            string[] result = Dictionnaire.TriBogo(tab1);
            for (int i = 0; i < tab2.Length; i++)
            {
                Assert.AreEqual(tab2[i], result[i]);
            }
        }
        [TestMethod]
        public void TestMethod4()
        {
            string[] tab1 = { "4", "7", "1", "9" };
            string[] tab2 = { "1", "4", "7", "9" };
            Dictionnaire.TriInsertion(tab1);
            for (int i = 0; i < tab2.Length; i++)
            {
                Assert.AreEqual(tab2[i], tab1[i]);
            }
        }
        [TestMethod]
        public void TestMethod5()
        {
            Dictionnaire dico = new Dictionnaire("Francais");
            bool result = dico.RechDichoRecursif("AMI", 0, dico.GetDictionnaire.Length);
            Assert.IsTrue(result);
            bool result2 = dico.RechDichoRecursif("zlkshjfzoi", 0, dico.GetDictionnaire.Length);
            Assert.IsFalse(result2);
        }

    }
}
