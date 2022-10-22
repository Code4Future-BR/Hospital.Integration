namespace Hospital.Integration.Infra.Data.Repositories.Shared.Statements;

public static class PatientStmt
{
    public const string SelectByParamns = @"
        SELECT 
            [Id],
            [Name],
            [Cpf],
            [Email],
            [PhoneNumber1],
            [PhoneNumber2],
            [PhoneNumber3],
            [Active]
        FROM [Patient]";

    public const string SelectByParamnsCount = @"
        SELECT 
            COUNT([Id])
        FROM [Patient]";
}
