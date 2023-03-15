namespace Teledon.models;

public class Volunteer : Entity<long>
{
    private String Name { get; set; }
    private String Username { get; set; }
    private String Password { get; set; }

    public Volunteer(long id, string name, string username, string password) : base(id)
    {
        Name = name;
        Username = username;
        Password = password;
    }
}