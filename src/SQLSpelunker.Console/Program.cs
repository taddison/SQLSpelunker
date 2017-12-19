using SQLSpelunker.Core;
using System;
using System.Data.SqlClient;

namespace SQLSpelunker.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "server=localhost;initial catalog=tsqlscheduler;integrated security=sspi";
            var sqlScript = "exec scheduler.UpsertJobsForAllTasks";

            if(args.Length > 0)
            {
                connectionString = args[0];
                sqlScript = args[1];
            }

            var builder = new SqlConnectionStringBuilder(connectionString);
            var database = builder.InitialCatalog;

            var sw = new ScriptWalker(new SQLDatabaseDefinitionService(connectionString));
            var callChain = sw.GetCalledProcedures(sqlScript, database);
            System.Console.WriteLine(callChain.GetCallHierarchy());
        }
    }
}
