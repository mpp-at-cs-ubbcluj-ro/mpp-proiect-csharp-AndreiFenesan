namespace Teledon.models;

public class Donor : Entity<long>
{
    public String Name { get; }
    public String EmailAddress { get; }
    public String PhoneNumber { get; }

    public Donor(long id, string name, string emailAddress, string phoneNumber) : base(id)
    {
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }

    public Donor(string name, string emailAddress, string phoneNumber) : base(-2)
    {
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + Name + "; " + EmailAddress + "; " + PhoneNumber;
    }
}