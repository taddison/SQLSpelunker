using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLSpelunker.Core
{
    public class ScriptWalker
    {
        private ISQLDefinitionService _definitionService;
        
        public ScriptWalker(ISQLDefinitionService definitionService)
        {
            _definitionService = definitionService ?? throw new ArgumentNullException(nameof(definitionService));
        }

        public ProcedureCall GetCalledProcedures(string sqlScript, string currentDatabase, string defaultSchema = "dbo")
        {
            if (string.IsNullOrWhiteSpace(sqlScript))
            {
                throw new ArgumentOutOfRangeException(sqlScript);
            }

            if (string.IsNullOrWhiteSpace(currentDatabase))
            {
                throw new ArgumentOutOfRangeException(currentDatabase);
            }

            if (string.IsNullOrWhiteSpace(defaultSchema))
            {
                throw new ArgumentOutOfRangeException(defaultSchema);
            }

            var call = new ProcedureCall(null,null,0);

            PopulateCalledProcedures(call, sqlScript, currentDatabase, defaultSchema);

            return call;
        }

        private void PopulateCalledProcedures(ProcedureCall call, string sqlScript, string currentDatabase, string defaultSchema)
        {
            var parsed = BatchParser.GetExecutedProcedures(sqlScript);
            var wellNamed = parsed.Select(p => new StoredProcedure(p, defaultSchema, currentDatabase));

            // Parent list for any children
            var newParentList = call.AllParents.ToList();
            if(!call.IsRoot)
            {
                newParentList.Add(call.StoredProcedure);
            }

            foreach(var proc in wellNamed)
            {
                // If we've already seen this call we're in an infinite loop
                if(newParentList.Contains(proc))
                {
                    var infinite = new ProcedureCall(newParentList, proc, call.Depth + 1)
                    {
                        IsInfiniteLoop = true
                    };
                    call.Children.Add(infinite);
                    continue;
                }

                //var definition = StoredProcedureParser.ExtractBodyOfProcedure(_definitionService.GetStoredProcedureDefinition(proc));
                var definition = _definitionService.GetStoredProcedureDefinition(proc);
                var childCall = new ProcedureCall(newParentList, proc, call.Depth + 1);
                call.Children.Add(childCall);
                PopulateCalledProcedures(childCall, definition, childCall.StoredProcedure.Database, defaultSchema);
            }
        }
    }
}
