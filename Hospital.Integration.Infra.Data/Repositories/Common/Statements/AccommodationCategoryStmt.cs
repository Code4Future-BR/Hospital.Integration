namespace Hospital.Integration.Infra.Data.Repositories.Shared.Statements;

public static class AccommodationCategoryStmt
{
    public const string SelectByParamns = @"
        SELECT 
            [Id],
            [Name],
            [Active]
        FROM [AccommodationCategory]";

    public const string SelectByParamnsCount = @"
        SELECT 
            COUNT([Id])
        FROM [AccommodationCategory]";
}
