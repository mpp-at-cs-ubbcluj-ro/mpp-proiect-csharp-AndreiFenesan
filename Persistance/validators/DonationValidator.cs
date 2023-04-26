namespace Persistance.validators;
using model;
public class DonationValidator:IValidator<Donation>
{
    public void Validate(Donation donation)
    {
        if (donation == null)
        {
            throw new ValidationException("Donation is null");
        }

        if (donation.Amount < 1)
        {
            throw new ValidationException("Amount must be a value greater then 1");
        }
    }
}