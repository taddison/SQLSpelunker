using System;
using System.Collections.Generic;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.IO;
using System.Linq;

namespace SQLSpelunker.Core
{
    public static class ProcedureParser
    {
        public static IList<ParsedStoredProcedureIdentifier> GetExecutedProcedures(string tsqlBatch)
        {
            var parser = new TSql140Parser(false);
            var sr = new StringReader(tsqlBatch);
            var tokens = parser.GetTokenStream(sr, out IList<ParseError> tokenErrors);
            var fragment = parser.Parse(tokens, out IList<ParseError> fragmentErrors);

            var procs = from batch in (fragment as TSqlScript).Batches
                                     from statement in batch.Statements
                                     let execStatement = statement as TSqlFragment as ExecuteStatement
                                     where execStatement != null
                                     let execProc = execStatement.ExecuteSpecification.ExecutableEntity as ExecutableProcedureReference
                                     select execProc;

            var executedProcs = new List<ParsedStoredProcedureIdentifier>();

            foreach(var proc in procs)
            {
                var id = proc.ProcedureReference.ProcedureReference.Name;
                var procedure = new ParsedStoredProcedureIdentifier(id.DatabaseIdentifier?.Value, id.SchemaIdentifier?.Value, id.BaseIdentifier.Value);
                executedProcs.Add(procedure);
            }

            return executedProcs;
        }
    }
}
