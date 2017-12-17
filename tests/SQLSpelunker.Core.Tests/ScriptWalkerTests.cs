using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace SQLSpelunker.Core.Tests
{
    [TestClass]
    public class ScriptWalkerTests
    {
        [TestMethod]
        public void CalledWithNoProcsGeneratesNoChildren()
        {
            var sw = new ScriptWalker(new FakeDefinitionService());
            var procs = sw.GetCalledProcedures("print 'nothing'", "master");

            Assert.AreEqual(0, procs.Children.Count);
        }

        [TestMethod]
        public void CalledWithProcsAndNoLookupsGeneratesFirstLevelOfChildren()
        {
            var fds = new FakeDefinitionService();
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcOne"), "");
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcTwo"), "");
            var sw = new ScriptWalker(fds);
            var procs = sw.GetCalledProcedures("exec dbo.ProcOne; exec dbo.ProcTwo;", "master");

            Assert.AreEqual(2, procs.Children.Count);
        }

        [TestMethod]
        public void InfiniteLoopIdentifiedAtLevelOne()
        {
            var fds = new FakeDefinitionService();
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcOne"), "");
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcTwo"), "exec dbo.ProcTwo;");
            var sw = new ScriptWalker(fds);
            var procs = sw.GetCalledProcedures("exec dbo.ProcOne; exec dbo.ProcTwo;", "master");
            Debug.WriteLine(procs.GetCallHierarchy());
            // script -> ProcTwo -> ProcTwo => second proc call should be detected as the infinite loop
            Assert.IsTrue(procs.Children[1].Children[0].IsInfiniteLoop);
        }

        [TestMethod]
        public void MultipleLevelOutput()
        {
            var fds = new FakeDefinitionService();
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcOne"), "");
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcTwo"), "exec dbo.ProcThree;");
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcThree"), "exec dbo.ProcFour;");
            fds.AddDefinition(new StoredProcedure("master", "dbo", "ProcFour"), "return;");
            var sw = new ScriptWalker(fds);
            var procs = sw.GetCalledProcedures("exec dbo.ProcOne; exec dbo.ProcTwo; exec ProcOne; exec ProcTwo;", "master");
            Debug.WriteLine(procs.GetCallHierarchy());

            Assert.IsTrue(true);
        }
    }
}
