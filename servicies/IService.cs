using Teledon.dtos;
using Teledon.models;

namespace Teledon.servicies;

public interface IService
{
   Boolean AuthenticateVolunteer(String username, String password);
   Boolean AddNewDonation(long charityCaseId, String name, String emailAddress, String phoneNumber, double donationAmount);
   IEnumerator<Donor> GetDonorsWithNameContaining(String containsInName);
   List<CharityCaseDto> GetAllCharityCases();
}