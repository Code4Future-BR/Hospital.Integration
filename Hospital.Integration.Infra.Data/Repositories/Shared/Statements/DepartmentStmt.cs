namespace Hospital.Integration.Infra.Data.Repositories.Shared.Statements;

public static class DepartmentStmt
{
    public const string Create = @"
        INSERT INTO [Department] 
        ( 
            [Id],
            [Name],
            [Active]                    
        )
        VALUES 
        (
            @Id,
            @Name,
            @Active  
        )";

    public const string SelectById = @"
        SELECT 
            [Id],
            [Name],
            [Active]
        FROM [Department] 
        WHERE 
            [Id] = @Id";

    public const string SelectByParamns = @"
        SELECT 
            [Id],
            [Name],
            [Active]
        FROM [Department]";

    public const string SelectByParamnsCount = @"
        SELECT 
            COUNT([Id]
        FROM [Department]";
}
