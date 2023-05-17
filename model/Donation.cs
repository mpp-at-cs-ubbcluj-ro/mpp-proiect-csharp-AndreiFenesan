namespace model;
public class Donation : Entity<long>
{
    public double Amount { get; private set; }
    public long DonorId { get; private set; }

    public long CharityCaseId { get; private set; }

    public Donation(long id, double amount, long donorId, long charityCaseId) : base(id)
    {
        Amount = amount;
        DonorId = donorId;
        CharityCaseId = charityCaseId;
    }

    public Donation(double amount, long donorId, long charityCaseId) : base(-2)
    {
        Amount = amount;
        DonorId = donorId;
        CharityCaseId = charityCaseId;
    }

    public override string ToString()
    {
        return base.ToString() + "; " + Amount + "; " + DonorId + "; " + CharityCaseId;
    }
}