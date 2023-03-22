using System.Data;
using log4net;
using Teledon.models;
using Teledon.repositories.interfaces;

namespace Teledon.repositories.db_implementations;

public class DonationDbRepository : IDonationRepository
{
    private DbUtils _dbUtils;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(DonationDbRepository));

    public DonationDbRepository(DbUtils dbUtils)
    {
        _dbUtils = dbUtils;
    }

    public Donation Save(Donation donation)
    {
        Logger.InfoFormat("Saving donation {0}", donation);
        IDbConnection connection = _dbUtils.GetConnection();
        string query =
            "INSERT INTO tables.donation (amount, charitycaseid, donorid) values (@amount,@charityCaseId,@donorId) returning *";
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;

            IDataParameter amountParameter = command.CreateParameter();
            amountParameter.ParameterName = "@amount";
            amountParameter.Value = donation.Amount;

            IDataParameter charityCaseIdParameter = command.CreateParameter();
            charityCaseIdParameter.ParameterName = "@charityCaseId";
            charityCaseIdParameter.Value = donation.CharityCaseId;

            IDataParameter donorIdParameter = command.CreateParameter();
            donorIdParameter.ParameterName = "@donorId";
            donorIdParameter.Value = donation.DonorId;

            command.Parameters.Add(amountParameter);
            command.Parameters.Add(charityCaseIdParameter);
            command.Parameters.Add(donorIdParameter);

            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Donation extractedDonation = ExtractDonationFromReader(reader);
                    Logger.InfoFormat("Saved {0} successfully", extractedDonation);
                    return extractedDonation;
                }
            }
        }

        Logger.InfoFormat("Couldn't save {0} ", donation);
        return null;
    }

    public Donation Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Donation Update(Donation entity)
    {
        throw new NotImplementedException();
    }

    public List<Donation> FindAll()
    {
        throw new NotImplementedException();
    }

    public Donation findOneById(long id)
    {
        throw new NotImplementedException();
    }

    public double GetTotalAmountOfMoneyRaised(long charityCaseId)
    {
        Logger.InfoFormat("Computing the total amount of money raised for charity with id {0}", charityCaseId);
        String query = "select SUM(amount) as totalAmount from tables.donation where charitycaseid=@charityCaseId";
        IDbConnection connection = _dbUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;

            IDataParameter charityCaseIdParameter = command.CreateParameter();
            charityCaseIdParameter.ParameterName = "charityCaseId";
            charityCaseIdParameter.Value = charityCaseId;

            command.Parameters.Add(charityCaseIdParameter);

            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    try
                    {
                        double totalAmount = reader.GetDouble(0);
                        Logger.InfoFormat("Computed sum {0}", totalAmount);
                        return totalAmount;
                    }
                    catch (InvalidCastException exception)
                    {
                        Logger.ErrorFormat("Error: {0}", exception);
                        return 0;
                    }
                }
            }
        }

        Logger.InfoFormat("The computed sum is {0}", 0);
        return 0;
    }

    private Donation ExtractDonationFromReader(IDataReader reader)
    {
        long id = reader.GetInt64(0);
        double amount = reader.GetDouble(1);
        long charityCaseId = reader.GetInt64(2);
        long donorId = reader.GetInt64(3);
        return new Donation(id, amount, donorId, charityCaseId);
    }
}