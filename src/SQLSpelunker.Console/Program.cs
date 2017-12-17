using SQLSpelunker.Core;
using System;

namespace SQLSpelunker.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "server=localhost;initial catalog=tsqlscheduler;integrated security=sspi";
            var sqlScript = "exec scheduler.UpsertJobsForAllTasks";
            var currentDatabase = "tsqlscheduler";

            if(args.Length > 0)
            {
                connectionString = args[0];
                sqlScript = args[1];
                currentDatabase = args[2];
            }

            var sw = new ScriptWalker(new SQLDatabaseDefinitionService(connectionString));
            var callChain = sw.GetCalledProcedures(sqlScript, currentDatabase);
            System.Console.WriteLine(callChain.GetCallHierarchy());
        }
    }
}
