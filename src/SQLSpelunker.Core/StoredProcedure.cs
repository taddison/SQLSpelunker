using System;

namespace SQLSpelunker.Core
{
    public class StoredProcedure
    {
        private static string DEFAULT_SCHEMA = "DEF_SCH";

        public StoredProcedure(string database, string schema, string name)
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

        public override string ToString()
        {
            if(string.IsNullOrWhiteSpace(Database))
            {
                return $"{Schema ?? DEFAULT_SCHEMA}.{Name}";
            }
            else
            {
                return $"{Database}.{Schema ?? DEFAULT_SCHEMA}.{Name}";
            }
        }
    }
}