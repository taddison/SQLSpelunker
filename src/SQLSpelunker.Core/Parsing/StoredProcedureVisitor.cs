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
            ExecutedProcedures = new List<ExecutableProcedureReference>();
            CreatedProcedures = new List<CreateProcedureStatement>();
        }

        public IList<ExecutableProcedureReference> ExecutedProcedures { get; private set; }
        public IList<CreateProcedureStatement> CreatedProcedures { get; private set; }

        public override void Visit(ExecutableProcedureReference node)
        {
            ExecutedProcedures.Add(node);
            base.Visit(node);
        }

        public override void Visit(CreateProcedureStatement node)
        {
            CreatedProcedures.Add(node);
            base.Visit(node);
        }
    }
}
