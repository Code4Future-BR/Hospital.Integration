namespace Hospital.Integration.Infra.Data.Repositories
{
    internal static class UserStmt
    {
        public const string SelectByEmail = @"
            SELECT TOP 1
                Id,
                FirstName,
                LastName,
                PhoneNumber,
                Email,
                Password,
                Active
            FROM [User]
            WHERE
                Email = @Email";

        public const string SelectValidateCredentials = @"
                SELECT TOP 1 CONVERT(INT, 1)
                FROM [User]
                WHERE
                    Email = @Email
                    AND Password = @Password";
    }
}
