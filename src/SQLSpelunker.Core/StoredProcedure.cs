using System;

namespace SQLSpelunker.Core
{
    public class StoredProcedure
    {
        public StoredProcedure(string database, string schema, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(schema))
            {
                throw new ArgumentOutOfRangeException(nameof(schema));
            }

            if (string.IsNullOrWhiteSpace(database))
            {
                throw new ArgumentOutOfRangeException(nameof(database));
            }

            Database = database;
            Schema = schema;
            Name = name;
        }

        public StoredProcedure(ParsedStoredProcedureIdentifier procedure)
            : this(procedure.Database, procedure.Schema, procedure.Name) { }

        public string Database { get; private set; }
        public string Schema { get; private set; }
        public string Name { get; private set; }

        public override int GetHashCode()
        {
            return $"{Database}.{Schema}.{Name}".ToLowerInvariant().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var proc = obj as StoredProcedure;
            if(proc == null)
            {
                return false;
            }

            return proc.Database.ToLowerInvariant() == Database.ToLowerInvariant()
                && proc.Schema.ToLowerInvariant() == Schema.ToLowerInvariant() 
                && proc.Name.ToLowerInvariant() == Name.ToLowerInvariant();
        }
    }
}
