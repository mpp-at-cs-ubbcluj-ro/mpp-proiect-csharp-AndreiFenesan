using System.Data;
using log4net;
using model;
using persistance.repositoryInterfaces;
using persistance.validators;

namespace persistance.database_repository;

public class DonorDbRepository : IDonorRepository
{
    private DbUtils _dbUtils;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(DonorDbRepository));
    private IValidator<Donor> donorValidator;

    public DonorDbRepository(DbUtils dbUtils, IValidator<Donor> donorValidator)
    {
        this.donorValidator = donorValidator;
        _dbUtils = dbUtils;
    }

    public Donor Save(Donor entity)
    {
        Logger.InfoFormat("Saving donor: {0}", entity);
        Logger.InfoFormat("Validating donor: {0}", entity);

        donorValidator.Validate(entity);
        Logger.InfoFormat("Valid donor: {0}", entity);

        string query =
            "INSERT INTO tables.donor (name, email, phonenumber) values (@name, @email, @phoneNumber) returning *";
        IDbConnection connection = _dbUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            IDataParameter nameParameter = command.CreateParameter();
            nameParameter.ParameterName = "@name";
            nameParameter.Value = entity.Name;

            IDataParameter emailParameter = command.CreateParameter();
            emailParameter.ParameterName = "@email";
            emailParameter.Value = entity.EmailAddress;

            IDataParameter phoneNumberParameter = command.CreateParameter();
            phoneNumberParameter.ParameterName = "@phoneNumber";
            phoneNumberParameter.Value = entity.PhoneNumber;

            command.Parameters.Add(nameParameter);
            command.Parameters.Add(emailParameter);
            command.Parameters.Add(phoneNumberParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Logger.InfoFormat("Donor: {0} saved successfully", entity);
                    return ExtractDonorFromReader(reader);
                }
            }
        }

        Logger.InfoFormat("Donor: {0} not saved", entity);
        return null;
    }


    public Donor Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Donor Update(Donor entity)
    {
        throw new NotImplementedException();
    }

    public List<Donor> FindAll()
    {
        throw new NotImplementedException();
    }

    public Donor? findOneById(long id)
    {
        Logger.InfoFormat("Finding donor by id {0}", id);
        IDbConnection connection = _dbUtils.GetConnection();
        string query = "SELECT * FROM tables.donor where id=@id";
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;

            IDataParameter idParameter = command.CreateParameter();
            idParameter.ParameterName = "@id";
            idParameter.Value = id;
            command.Parameters.Add(idParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Donor donor = ExtractDonorFromReader(reader);
                    Logger.InfoFormat("Found donor by id {0}", donor);
                    return donor;
                }
            }
        }

        Logger.InfoFormat("No donor found by id {0}", id);
        return null;
    }

    public IEnumerator<Donor> FindDonorByNameLike(string pattern)
    {
        Logger.InfoFormat("searching for donor with name pattern: {0}", pattern);
        List<Donor> donors = new List<Donor>();
        string query = "SELECT * from tables.donor where name ilike @pattern";
        IDbConnection connection = _dbUtils.GetConnection();
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            IDataParameter patternParameter = command.CreateParameter();
            patternParameter.ParameterName = "@pattern";
            patternParameter.Value = pattern;
            command.Parameters.Add(patternParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    donors.Add(ExtractDonorFromReader(reader));
                }
            }
        }

        return donors.GetEnumerator();
    }

    public Donor? FindDonorByPhoneNumber(string phoneNumber)
    {
        Logger.InfoFormat("Finding donor by phoneNumber {0}", phoneNumber);
        IDbConnection connection = _dbUtils.GetConnection();
        string query = "SELECT * FROM tables.donor where phonenumber=@phoneNumber";
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;

            IDataParameter phoneNumberParameter = command.CreateParameter();
            phoneNumberParameter.ParameterName = "@phoneNumber";
            phoneNumberParameter.Value = phoneNumber;
            command.Parameters.Add(phoneNumberParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Donor donor = ExtractDonorFromReader(reader);
                    Logger.InfoFormat("Found donor by phoneNumber {0}", donor);
                    return donor;
                }
            }
        }

        Logger.InfoFormat("No donor found by phoneNumber {0}", phoneNumber);
        return null;
    }

    private Donor ExtractDonorFromReader(IDataReader reader)
    {
        long id = reader.GetInt64(0);
        string name = reader.GetString(1);
        string email = reader.GetString(2);
        string phoneNumber = reader.GetString(3);
        return new Donor(id, name, email, phoneNumber);
    }
}