using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SQLSpelunker.Core.Tests
{
    [TestClass]
    public class StoredProcedureTests
    {
        [TestMethod]
        public void StoredProcsOfDifferentCaseAreEqual()
        {
            var procOne = new StoredProcedure("DB1", "dbo", "PROCONE");
            var procTwo = new StoredProcedure("db1", "dbo", "procone");

            Assert.AreEqual(procOne, procTwo);
        }

        [TestMethod]
        public void HashtableLookupsOfDifferentCaseMatch()
        {
            var procOne = new StoredProcedure("DB1", "dbo", "PROCONE");
            var dic = new Dictionary<StoredProcedure, string>();
            dic.Add(procOne, "this is a test");

            var procTwo = new StoredProcedure("db1", "dbo", "procone");

            Assert.IsTrue(dic.ContainsKey(procTwo));
        }
    }
}
