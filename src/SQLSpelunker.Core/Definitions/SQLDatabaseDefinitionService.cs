using System;
using System.Collections.Generic;
using System.Text;

namespace SQLSpelunker.Core
{
    public class SQLDatabaseDefinitionService : ISQLDefinitionService
    {
        private string _connectionString;
        private Dictionary<StoredProcedure, string> _definitions;

        public SQLDatabaseDefinitionService(string connectionString)
        {
            if(String.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentOutOfRangeException(nameof(connectionString));
            }
            _connectionString = connectionString;
            _definitions = new Dictionary<StoredProcedure, string>();
        }

        public string GetStoredProcedureDefinition(StoredProcedure storedProcedure)
        {
            if(storedProcedure == null)
            {
                throw new ArgumentNullException(nameof(storedProcedure));
            }

            if(_definitions.TryGetValue(storedProcedure, out string definition))
            {
                return definition;
            }

            return ""; // TODO: Get the definition from the database & store it
        }
    }
}
