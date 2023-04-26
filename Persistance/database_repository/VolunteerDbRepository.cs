using System.Data;
using log4net;
using model;
using Persistance.interfaces;

namespace Persistance.database_repository;

public class VolunteerDbRepository : IVolunteerRepository
{
    private DbUtils _dbUtils;
    private static readonly ILog Logger = LogManager.GetLogger(typeof(VolunteerDbRepository));

    public VolunteerDbRepository(DbUtils dbUtils)
    {
        _dbUtils = dbUtils;
    }

    public Volunteer Save(Volunteer entity)
    {
        throw new NotImplementedException();
    }

    public Volunteer Delete(long id)
    {
        throw new NotImplementedException();
    }

    public Volunteer Update(Volunteer entity)
    {
        throw new NotImplementedException();
    }

    public List<Volunteer> FindAll()
    {
        throw new NotImplementedException();
    }

    public Volunteer findOneById(long id)
    {
        throw new NotImplementedException();
    }

    public Volunteer? FindVolunteerByUsername(string username)
    {
        Logger.InfoFormat("Find volunteer by useername: {0}", username);
        IDbConnection connection = _dbUtils.GetConnection();
        String query = "SELECT * FROM tables.volunteer where username=@username";
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            IDbDataParameter usernameParameter = command.CreateParameter();
            usernameParameter.ParameterName = "@username";
            usernameParameter.Value = username;

            command.Parameters.Add(usernameParameter);
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Volunteer volunteer = ExtractVolunteerFromReader(reader);
                    Logger.InfoFormat("Found volunteer {0}", volunteer);
                    return volunteer;
                }
            }
        }

        Logger.InfoFormat("No volunteer found with username {0}", username);
        return null;
    }

    private Volunteer ExtractVolunteerFromReader(IDataReader reader)
    {
        long id = reader.GetInt64(0);
        string name = reader.GetString(1);
        string username = reader.GetString(2);
        string password = reader.GetString(3);
        return new Volunteer(id, name, username, password);
    }
}