namespace Hospital.Integration.Domain.Commons;

public class Department : Entity
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public bool Active { get; private set; }

    public Department (string id, string name, bool active)
    {
        Id = id;
        Name = name;
        Active = active;
    }
}
