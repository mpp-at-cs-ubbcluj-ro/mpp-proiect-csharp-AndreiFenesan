namespace Persistance.interfaces;
using model;
public interface IDonationRepository:IRepository<long,Donation>
{
    double GetTotalAmountOfMoneyRaised(long charityCaseId);
}