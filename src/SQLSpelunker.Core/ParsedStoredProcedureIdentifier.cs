using System;

namespace SQLSpelunker.Core
{
    public class ParsedStoredProcedureIdentifier
    {
        public ParsedStoredProcedureIdentifier(string database, string schema, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            // Default schema (e.g. DB..Proc) parses as an empty string, use null instead
            if(string.IsNullOrWhiteSpace(schema))
            {
                schema = null;
            }

            Database = database;
            Schema = schema;
            Name = name;
        }

        public string Database { get; private set; }
        public string Schema { get; private set; }
        public string Name { get; private set; }

        public bool IsDefaultSchema => String.IsNullOrWhiteSpace(Schema);
        public bool IsCurrentDatabase => String.IsNullOrWhiteSpace(Database);
    }
}