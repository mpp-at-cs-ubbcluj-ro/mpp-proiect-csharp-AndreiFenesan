namespace Persistance.interfaces;
using model;
public interface IDonorRepository : IRepository<long, Donor>
{
    IEnumerator<Donor> FindDonorByNameLike(string pattern);
    Donor? FindDonorByPhoneNumber(string phoneNumber);
}