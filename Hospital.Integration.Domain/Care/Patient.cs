using Hospital.Integration.Domain.Commons;

namespace Hospital.Integration.Domain.Care;

public class Patient : Entity
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public string Gender { get; private set; }

    public string? Cpf { get; private set; }

    public DateOnly BirthDate { get; private set; }

    public string? MotherName { get; private set; }

    public Address Address { get; private set; }

    public Patient() { }

    public Patient(string id, string name, string gender, 
        string? cpf, DateOnly birthDate, string? motherName,
        Address address)
    {
        Id = id;
        Name = name;
        Gender = gender;
        Cpf = cpf;
        BirthDate = birthDate;
        MotherName = motherName;
        Address = address;
    }
}
