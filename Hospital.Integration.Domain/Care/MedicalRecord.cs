namespace Hospital.Integration.Domain.Care;

public class MedicalRecord : Entity, IAggregateRoot
{
    public string Id { get; private set; }

    public DateTimeOffset EntryDateTime { get; private set; }

    public Patient Patient {get; private set;}

    public HealthPlan HealthPlan {get; private set;}

}
