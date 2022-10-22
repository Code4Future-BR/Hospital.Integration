namespace Hospital.Integration.Infra.Data.Repositories.Shared.Statements;

public static class AccommodationStmt
{
    public const string SelectByParamns = @"
        SELECT 
            [Id],
            [Name],
            [Active]
        FROM [Accommodation]";

    public const string SelectByParamnsCount = @"
        SELECT 
            COUNT([Id])
        FROM [Accommodation]";
}
