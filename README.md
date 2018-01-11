# SQLSpelunker
Spelunk your MSSQL code.

Supports visualising the call tree of one/more stored procedures.

# Usage
Command line usage requires two arguments:
- **c**onnstr - Connection string (must specify database/initial catalog)
- **s**cript - SQL script to parse

```powershell
c:\source\SQLSpelunker\src\SQLSpelunker.Console>dotnet run --c "server=localhost;initial catalog=foo;integrated security=sspi" --s "exec dbo.SomeProc"
```