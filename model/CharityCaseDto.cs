namespace model;

public class CharityCaseDto
{
    public long Id { get; }
    public String CaseName { get; }
    public String Description { get; }
    public double TotalAmountCollected { get; }

    public CharityCaseDto(double totalAmountCollected, long id, string caseName, string description)
    {
        this.TotalAmountCollected = totalAmountCollected;
        this.Id = id;
        this.CaseName = caseName;
        this.Description = description;
    }

    public override string ToString()
    {
        return Id + " " + CaseName + " " + Description + " " + TotalAmountCollected;
    }
}