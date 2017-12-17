using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SQLSpelunker.Core
{
    public static class StoredProcedureParser
    {
        public static string ExtractBodyOfProcedure(string procedureDefinition)
        {
            if(string.IsNullOrWhiteSpace(procedureDefinition))
            {
                throw new ArgumentOutOfRangeException(nameof(procedureDefinition));
            }

            var parser = new TSql140Parser(false);
            var sr = new StringReader(procedureDefinition);
            var fragment = parser.Parse(sr, out IList<ParseError> fragmentErrors);

            var createProc = (fragment as TSqlScript)?.Batches[0]?.Statements[0] as CreateProcedureStatement;
            if(createProc == null)
            {
                throw new Exception("string must contain a procedure definition");
            }

            return "";
        }
    }
}
