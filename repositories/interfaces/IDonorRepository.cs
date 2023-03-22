using Teledon.models;

namespace Teledon.repositories.interfaces;

public interface IDonorRepository : IRepository<long, Donor>
{
    IEnumerator<Donor> FindDonorByNameLike(string pattern);
    Donor? FindDonorByPhoneNumber(string phoneNumber);
}