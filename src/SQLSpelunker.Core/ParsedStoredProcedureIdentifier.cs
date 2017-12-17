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

            Database = database;
            Schema = schema;
            Name = name;
        }

        public string Database { get; set; }
        public string Schema { get; set; }
        public string Name { get; set; }

        public bool IsDefaultSchema => String.IsNullOrWhiteSpace(Schema);
        public bool IsCurrentDatabase => String.IsNullOrWhiteSpace(Database);
    }
}