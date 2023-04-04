using System;

namespace lab5.dtos;

public class CharityCaseDto
{
    public long Id { get; set; }
    public String CaseName { get; set; }
    public String Description { get; set; }
    public double TotalAmountCollected { get; set; }

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