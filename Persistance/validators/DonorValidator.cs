namespace Persistance.validators;
using model;
public class DonorValidator : IValidator<Donor>
{
    public void Validate(Donor donor)
    {
        CheckForNullAndEmptyStrings(donor);
    }

    private void CheckForNullAndEmptyStrings(Donor donor)
    {
        if (String.IsNullOrWhiteSpace(donor.Name))
        {
            throw new ValidationException("Invalid name");
        }

        String donorEmailAddress = donor.EmailAddress;
        if (String.IsNullOrWhiteSpace(donorEmailAddress) || donorEmailAddress.Length < 5 ||
            !donorEmailAddress.Contains('@'))
        {
            throw new ValidationException("Invalid email address");
        }

        String donorPhoneNumber = donor.PhoneNumber;
        if (String.IsNullOrWhiteSpace(donorPhoneNumber) || donorPhoneNumber.Length < 7 ||
            donorPhoneNumber.Length > 20)
        {
            throw new ValidationException("Invalid phone number");
        }
    }
}