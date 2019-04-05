# SQLSpelunker
Spelunk your MSSQL code.

Supports visualising the call tree of one/more stored procedures.

# Usage
Command line usage requires two arguments:
- **C**onnstr - Connection string (must specify database/initial catalog)
- **S**cript - SQL script to parse

```powershell
c:\...\SQLSpelunker.Console>dotnet run --c "server=localhost;integrated security=sspi" --s "exec dbo.SomeProc"
```
