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

            var visitor = new StoredProcedureVisitor();
            fragment.Accept(visitor);

            if(visitor.CreatedProcedures.Count != 1)
            {
                throw new Exception("string must contain exactly one create procedure statement");
            }

            var create = visitor.CreatedProcedures[0];
            if(create.StatementList.Statements.Count == 0)
            {
                return "";
            }

            var firstToken = create.StatementList.Statements[0].FirstTokenIndex;
            var lastToken = create.StatementList.Statements[0].LastTokenIndex;

            var sb = new StringBuilder();
            for(var i = firstToken; i <= lastToken; i++)
            {
                sb.Append(create.ScriptTokenStream[i].Text);
            }

            return sb.ToString();
        }
    }
}
