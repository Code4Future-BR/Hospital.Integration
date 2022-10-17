using Hospital.Integration.Domain.Commons;

namespace Hospital.Integration.Domain.Care;

public class HealthPlan : Entity
{
    public string Id { get; private set; }

    public string Cnpj { get; private set; }

    public string Name { get; private set; }

    public Address Address { get; private set; }

    public string? Email { get; private set; }

    public string? PhoneNumber { get; private set; }

    public bool Active { get; private set; }

    public HealthPlan() { }

    public HealthPlan(string id, string cnpj, string name, 
        Address address, string? email, string phoneNumber,
        bool active)
    {
        Id = id;
        Cnpj = cnpj;
        Name = name;
        Address = address;
        Email = email;
        PhoneNumber = phoneNumber;
        Active = active;
    }
}
