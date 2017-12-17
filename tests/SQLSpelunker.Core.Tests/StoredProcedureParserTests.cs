using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SQLSpelunker.Core.Tests
{
    [TestClass]
    public class StoredProcedureParserTests
    {
        [TestMethod]
        public void SimpleExtractionWorks()
        {
            var procText = 
@"
create procedure dbo.ProcOne
as
exec dbo.ProcTwo;
";
            var extractedText = StoredProcedureParser.ExtractBodyOfProcedure(procText);

            Assert.AreEqual("exec dbo.ProcTwo;", extractedText);
        }
    }
}
