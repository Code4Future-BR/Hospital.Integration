namespace Hospital.Integration.Domain.Commons;

public class Accommodation : Entity
{
    public string? Id { get; private set; }

    public string Name { get; private set; }

    public bool Active { get; private set; }

    public Accommodation(string id, string name, bool active)
    {
        Id = id;
        Name = name;
        Active = active;
    }
}
