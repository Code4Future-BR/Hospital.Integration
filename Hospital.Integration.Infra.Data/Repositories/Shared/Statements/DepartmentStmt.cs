namespace Hospital.Integration.Infra.Data.Repositories.Shared.Statements;

public static class DepartmentStmt
{
    public const string Create = @"
        INSERT INTO Department 
        ( 
            Id,
            Name,
            Active                    
        )
        VALUES 
        (
            @Id,
            @Name,
            @Active  
        )";
}
