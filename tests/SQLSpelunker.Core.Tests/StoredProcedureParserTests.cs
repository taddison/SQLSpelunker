using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SQLSpelunker.Core.Tests
{
    [TestClass]
    public class StoredProcedureParserTests
    {
        [TestMethod]
        public void SingleExecStatementExtracted()
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

        [TestMethod]
        public void SingleExecStatementExtractedWithBeginEnd()
        {
            var procText =
@"
create procedure dbo.ProcOne
as
begin
exec dbo.ProcTwo;
end
";
            var extractedText = StoredProcedureParser.ExtractBodyOfProcedure(procText);

            Assert.AreEqual("begin\r\nexec dbo.ProcTwo;\r\nend", extractedText);
        }

        [TestMethod]
        public void SingleExecStatementExtractedWithBeginEndParamBlock()
        {
            var procText =
@"
create procedure dbo.ProcOne
    @someParam int
as
begin
exec dbo.ProcTwo;
end
";
            var extractedText = StoredProcedureParser.ExtractBodyOfProcedure(procText);

            Assert.AreEqual("begin\r\nexec dbo.ProcTwo;\r\nend", extractedText);
        }

        [TestMethod]
        public void EmptyProcReturnsEmptyString()
        {
            var procText =
@"
create procedure dbo.ProcOne
    @someParam int
as
";
            var extractedText = StoredProcedureParser.ExtractBodyOfProcedure(procText);

            Assert.AreEqual("", extractedText);
        }
    }
}
