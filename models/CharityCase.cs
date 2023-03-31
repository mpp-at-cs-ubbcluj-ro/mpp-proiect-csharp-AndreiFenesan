namespace Teledon.models;

public class CharityCase : Entity<long>
{
    public string CaseName { get; set; }
    public string Description { get; set; }

    public CharityCase(long id, string caseName, string description) : base(id)
    {
        this.CaseName = caseName;
        this.Description = description;
    }

    public CharityCase(string caseName, string description) : base(-2)
    {
        this.CaseName = caseName;
        this.Description = description;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + CaseName + "; " + Description;
    }
}