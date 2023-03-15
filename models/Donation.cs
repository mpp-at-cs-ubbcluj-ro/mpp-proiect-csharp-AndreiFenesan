namespace Teledon.models;

public class Donation : Entity<long>
{
    private double Amount { get; set; }
    private long DonorId { get; set; }

    public Donation(long id, double amount, long donorId) : base(id)
    {
        Amount = amount;
        DonorId = donorId;
    }
}