using SQLSpelunker.Core;
using System;
using System.Data.SqlClient;

namespace SQLSpelunker.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = string.Empty;
            var sqlScript = string.Empty;

            if(args.Length == 2)
            {
                connectionString = args[0];
                sqlScript = args[1];
            }
            else
            {
                WriteError("Invalid arguments");
                WriteError("dotnet run <connection string> <sql script>");
                return;
            }

            SqlConnectionStringBuilder builder = null;
            try
            {
                builder = new SqlConnectionStringBuilder(connectionString);
            }
            catch (ArgumentException ae)
            {
                WriteError($"Connection string is not valid: {ae.Message}");
                return;
            }
            var database = builder.InitialCatalog;

            if(string.IsNullOrWhiteSpace(database))
            {
                System.Console.WriteLine("Connection string does not specify database, defaulting to master");
                database = "master";
            }

            // Establish connection
            ScriptWalker sw = null;
            try
            {
                sw = new ScriptWalker(new SQLDatabaseDefinitionService(connectionString));
            }
            catch (SqlException sx)
            {
                WriteError($"Unable to connect to database: {sx.Message}");
                return;
            }
            var callChain = sw.GetCalledProcedures(sqlScript, database);
            System.Console.WriteLine(callChain.GetCallHierarchy());
        }

        private static void WriteError(string error)
        {
            var originalColour = System.Console.ForegroundColor;
            System.Console.Error.WriteLine(error);
            System.Console.ForegroundColor = originalColour;
        }
    }
}
