using lab5.models;

namespace lab5.repositories.interfaces;

public interface IDonationRepository:IRepository<long,Donation>
{
    double GetTotalAmountOfMoneyRaised(long charityCaseId);
}