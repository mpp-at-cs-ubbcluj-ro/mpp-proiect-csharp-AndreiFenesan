namespace Networking.requests;

[Serializable]
public class NewDonationRequest : IRequest
{
    public long CharityCaseId { get; }
    public string Name { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public double Amount { get; }

    public NewDonationRequest(long charityCaseId, string name, string email, string phoneNumber, double amount)
    {
        CharityCaseId = charityCaseId;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Amount = amount;
    }
}