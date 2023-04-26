namespace Servicies;

using model;

public interface IService : IObservable
{
    Boolean AuthenticateVolunteer(String username, String password);

    Boolean AddNewDonation(long charityCaseId, String name, String emailAddress, String phoneNumber,
        double donationAmount);

    IEnumerator<Donor> GetDonorsWithNameContaining(String containsInName);
    List<CharityCaseDto> GetAllCharityCases();
}