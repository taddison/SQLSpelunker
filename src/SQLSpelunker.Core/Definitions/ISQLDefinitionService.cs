namespace SQLSpelunker.Core
{
    public interface ISQLDefinitionService
    {
        string GetStoredProcedureDefinition(StoredProcedure storedProcedure);
    }
}
