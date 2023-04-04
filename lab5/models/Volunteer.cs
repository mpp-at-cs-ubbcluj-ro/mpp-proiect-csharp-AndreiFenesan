using System;

namespace lab5.models;

public class Volunteer : Entity<long>
{
    public String Name { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }

    public Volunteer(long id, string name, string username, string password) : base(id)
    {
        Name = name;
        Username = username;
        Password = password;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + Name + "; " + Username + "; " + Password;
    }
}