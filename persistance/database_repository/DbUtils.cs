using System.Data;
using log4net;
using Npgsql;

namespace persistance.database_repository;

public class DbUtils
{
    private static readonly ILog Logger = LogManager.GetLogger(typeof(DbUtils));
    private IDbConnection? _dbConnectionInstance;
    private IDictionary<string, string> _props;

    public DbUtils(IDictionary<string, string> props)
    {
        _dbConnectionInstance = null;
        this._props = props;
    }

    private IDbConnection? GetNewConnection()
    {
        _props.TryGetValue("connectionString", out string? connectionString);
        Logger.InfoFormat("Connecting to {0}", connectionString);
        IDbConnection? connection = null;
        if (_dbConnectionInstance == null || _dbConnectionInstance.State == System.Data.ConnectionState.Closed)
        {
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
            Logger.InfoFormat("Connected successfully to {0}", connectionString);
        }

        return connection;
    }

    public IDbConnection GetConnection()
    {
        if (_dbConnectionInstance == null)
        {
            _dbConnectionInstance = GetNewConnection();
        }

        return _dbConnectionInstance;
    }
}