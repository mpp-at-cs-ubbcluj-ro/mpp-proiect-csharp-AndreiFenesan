namespace model;
[Serializable]
public class DonorDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public DonorDto(string name, string email, string phoneNumber)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public override string ToString()
    {
        return $"{Name} {Email} {PhoneNumber}";
    }
}