using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using lab5.models;
using lab5.repositories.interfaces;
using lab5.validators;

namespace lab5.repositories.db_implementations;

public class CharityDbRepository : ICharityRepository
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(CharityDbRepository));
    private DbUtils _dbUtils;

    public CharityDbRepository(DbUtils dbUtils)
    {
        _dbUtils = dbUtils;
    }

    public CharityCase Save(CharityCase entity)
    {
        throw new NotImplementedException();
    }

    public CharityCase Delete(long id)
    {
        throw new NotImplementedException();
    }

    public CharityCase Update(CharityCase entity)
    {
        throw new NotImplementedException();
    }

    public List<CharityCase> FindAll()
    {
        Logger.Info("Finding all Charity cases");
        List<CharityCase> charityCases = new List<CharityCase>();
        IDbConnection connection = _dbUtils.GetConnection();
        string query = "SELECT * FROM tables.charitycase";
        using (IDbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            using (IDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    charityCases.Add(ExtractCharityCaseFromReader(reader));
                }
            }
        }

        return charityCases;
    }

    private CharityCase ExtractCharityCaseFromReader(IDataReader reader)
    {
        long id = reader.GetInt64(0);
        string caseName = reader.GetString(1);
        string description = reader.GetString(2);
        return new CharityCase(id, caseName, description);
    }

    public CharityCase? findOneById(long id)
    {
        Logger.InfoFormat("Finding charity case with id: {0}", id);
        IDbConnection connection = _dbUtils.GetConnection();
        string query = "SELECT * FROM tables.charitycase where id = @id";
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
                    return ExtractCharityCaseFromReader(reader);
                }
            }
        }

        return null;
    }
}