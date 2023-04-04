using System.Collections.Generic;
using lab5.models;

namespace lab5.repositories.interfaces;

public interface IDonorRepository : IRepository<long, Donor>
{
    IEnumerator<Donor> FindDonorByNameLike(string pattern);
    Donor? FindDonorByPhoneNumber(string phoneNumber);
}