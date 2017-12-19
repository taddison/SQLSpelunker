# SQLSpelunker
Spelunk your MSSQL code

# Usage
Command line usage requires three arguments:
- Connection string (must specify database/initial catalog)
- SQL script to parse

```powershell
c:\source\SQLSpelunker\src\SQLSpelunker.Console>dotnet run "server=localhost;initial catalog=foo;integrated security=sspi" "exec dbo.SomeProc"
```