using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SQLSpelunker.Core.Tests
{
    [TestClass]
    public class StoredProcedureTests
    {
        [TestMethod]
        public void ThreePartNameToStringIsCorrect()
        {
            var proc = new StoredProcedure("DB1", "dbo", "ProcOne");
            
            Assert.AreEqual("DB1.dbo.ProcOne", proc.ToString());
        }

        [TestMethod]
        public void MissingSchemaNameIsCorrect()
        {
            var proc = new StoredProcedure(null, null, "ProcOne");

            Assert.AreEqual("DEF_SCH.ProcOne", proc.ToString());
        }

        [TestMethod]
        public void MissingSchemaNameWithDBIsCorrect()
        {
            var proc = new StoredProcedure("DB1", null, "ProcOne");

            Assert.AreEqual("DB1.DEF_SCH.ProcOne", proc.ToString());
        }
    }
}
