using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SQLSpelunker.Core.Tests
{
    [TestClass]
    public class BatchParserStoredProcedureTests
    {
        [TestMethod]
        public void AnEmptyStringReturnsNoProcedures()
        {
            var batchToParse = "";
            
            var procedures = BatchParser.GetExecutedProcedures(batchToParse);

            Assert.AreEqual(0, procedures.Count);
        }

        [TestMethod]
        public void ProcNameWithExecReturnsProcCount()
        {
            var batchToParse = "exec dbo.ProcOne;";

            var procedures = BatchParser.GetExecutedProcedures(batchToParse);

            Assert.AreEqual(1, procedures.Count);
        }

        [TestMethod]
        public void ProcWithExecNoSchemaReturnsProcCount()
        {
            var batchToParse = "exec ProcOne;";

            var procedures = BatchParser.GetExecutedProcedures(batchToParse);

            Assert.AreEqual(1, procedures.Count);
        }

        [TestMethod]
        public void ProcWithNoExecReturnsProcCount()
        {
            var batchToParse = "dbo.ProcOne;";

            var procedures = BatchParser.GetExecutedProcedures(batchToParse);

            Assert.AreEqual(1, procedures.Count);
        }

        [TestMethod]
        public void InvalidSQLReturnsNoProcedures()
        {
            var batchToParse = "this is not a valid pieceof SQL";

            var procedures = BatchParser.GetExecutedProcedures(batchToParse);

            Assert.AreEqual(0, procedures.Count);
        }

        [TestMethod]
        public void MultipleExecReturnsCorrectCount()
        {
            var batchToParse = "exec dbo.ProcOne exec dbo.ProcTwo";

            var procedures = BatchParser.GetExecutedProcedures(batchToParse);

            Assert.AreEqual(2, procedures.Count);
        }

        [TestMethod]
        public void DefaultSchemaNameParsedCorrectly()
        {
            var batchToParse = "exec DB1..ProcOne";

            var procedures = BatchParser.GetExecutedProcedures(batchToParse);

            Assert.AreEqual(1, procedures.Count);
        }
    }
}
