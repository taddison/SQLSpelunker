using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQLSpelunker.Core
{
    class StoredProcedureVisitor : TSqlConcreteFragmentVisitor
    {
        public StoredProcedureVisitor()
        {
            Procedures = new List<ExecutableProcedureReference>();
        }

        public IList<ExecutableProcedureReference> Procedures { get; private set; }

        public override void Visit(ExecutableProcedureReference node)
        {
            Procedures.Add(node);
            base.Visit(node);
        }
    }
}
