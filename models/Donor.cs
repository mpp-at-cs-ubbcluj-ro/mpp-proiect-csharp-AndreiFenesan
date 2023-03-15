namespace Teledon.models;

public class Donor : Entity<long>
{
    private String Name { get; set; }
    private String EmailAddress { get; set; }
    private String PhoneNumber { get; set; }

    public Donor(long id, string name, string emailAddress, string phoneNumber) : base(id)
    {
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
    }
}