using CommandLine;
using SQLSpelunker.Core;
using System;
using System.Data.SqlClient;

namespace SQLSpelunker.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);
            
            if(result.Tag == ParserResultType.NotParsed)
            {
                return;
            }

            var options = (result as Parsed<Options>).Value;
            
            SqlConnectionStringBuilder builder = null;
            try
            {
                builder = new SqlConnectionStringBuilder(options.Server);
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
                sw = new ScriptWalker(new SQLDatabaseDefinitionService(options.Server));
            }
            catch (SqlException sx)
            {
                WriteError($"Unable to connect to database: {sx.Message}");
                return;
            }
            var callChain = sw.GetCalledProcedures(options.Script, database);
            System.Console.WriteLine(callChain.GetCallHierarchy());
        }

        private static void WriteError(string error)
        {
            var originalColour = System.Console.ForegroundColor;
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.Error.WriteLine(error);
            System.Console.ForegroundColor = originalColour;
        }
    }
}
