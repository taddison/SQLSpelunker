using System;
using System.Collections.Generic;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.IO;
using System.Linq;

namespace SQLSpelunker.Core
{
    public static class BatchParser
    {
        public static IList<ParsedStoredProcedureIdentifier> GetExecutedProcedures(string tsqlBatch)
        {
            var parser = new TSql140Parser(false);
            var sr = new StringReader(tsqlBatch);
            var fragment = parser.Parse(sr, out IList<ParseError> fragmentErrors);

            var visitor = new StoredProcedureVisitor();
            fragment.Accept(visitor);

            var executedProcs = new List<ParsedStoredProcedureIdentifier>();

            foreach(var proc in visitor.Procedures)
            {
                var id = proc.ProcedureReference.ProcedureReference.Name;
                var procedure = new ParsedStoredProcedureIdentifier(id.DatabaseIdentifier?.Value, id.SchemaIdentifier?.Value, id.BaseIdentifier.Value);
                executedProcs.Add(procedure);
            }

            return executedProcs;
        }
    }
}
