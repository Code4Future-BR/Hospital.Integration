namespace Hospital.Integration.Domain.Security;

public class User : Entity, IAggregateRoot
{
    // public string Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string? Password { get; private set; }
    public bool Active { get; private set; }

    public User() { }

    public User(string id, string firstName, string lastName, 
        string phoneNumber, string email, 
        bool active)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Active = active;
    }

    public void ChangePassword(string passwordHash)
    {
        Password = passwordHash;
    }

    public void Update(string firstName, string lastName,
        string phoneNumber, string email,
        bool active)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
        Active = active;
    }
}
