using CommandLine;

namespace SQLSpelunker.Console
{
    public class Options
    {
        [Option('c', "connstr", Required = true, HelpText = "Connection string")]
        public string Server { get; set; }

        [Option('s', "script", Required = true, HelpText = "Script to parse")]
        public string Script { get; set; }
    }
}