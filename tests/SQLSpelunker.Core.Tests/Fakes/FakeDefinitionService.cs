using System.Collections.Generic;

namespace SQLSpelunker.Core.Tests
{
    public class FakeDefinitionService : ISQLDefinitionService
    {
        private Dictionary<StoredProcedure, string> _definitions = new Dictionary<StoredProcedure, string>();

        public string GetStoredProcedureDefinition(StoredProcedure storedProcedure)
        {
            return _definitions[storedProcedure];
        }

        public void AddDefinition(StoredProcedure storedProcedure, string definition)
        {
            _definitions.Add(storedProcedure, definition);
        }
    }
}
