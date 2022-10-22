namespace Hospital.Integration.Domain.Commons;

public class Professional : Entity
{
    public string? Id { get; private set; }

    public string Name { get; private set; }

    public string Cpf { get; private set; }

    public string Email { get; private set; 
        }
    public string PhoneNumber1 { get; private set; }

    public string PhoneNumber2 { get; private set; }

    public string PhoneNumber3 { get; private set; }

    public bool Active { get; private set; }

    public Professional(string id, string name, string cpf,
        string email, string phoneNumber1, string phoneNumber2,
        string phoneNumber3, bool active)
    {
        Id = id;
        Name = name;
        Cpf = cpf;
        Email = email;
        PhoneNumber1 = phoneNumber1;
        PhoneNumber2 = phoneNumber2;
        PhoneNumber3 = phoneNumber3;
        Active = active;
    }
}
