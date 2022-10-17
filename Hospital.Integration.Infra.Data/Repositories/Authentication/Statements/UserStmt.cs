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
            FROM [User] NOLOCK
            WHERE
                Email = @Email";

        public const string SelectValidateCredential = @"
                SELECT TOP 1 CONVERT(INT, 1)
                FROM [User] NOLOCK
                WHERE
                    Email = @Email
                    AND Password = @Password";
    }
}
