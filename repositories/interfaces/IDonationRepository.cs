using Teledon.models;

namespace Teledon.repositories.interfaces;

public interface IDonationRepository:IRepository<long,Donation>
{
    double GetTotalAmountOfMoneyRaised(long charityCaseId);
}