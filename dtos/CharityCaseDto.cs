namespace Teledon.dtos;

public class CharityCaseDto
{
    private long Id { get; }
    private String CaseName { get; }
    private String Description { get; }
    private double TotalAmountCollected { get; }

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