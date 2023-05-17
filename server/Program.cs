// See https://aka.ms/new-console-template for more information


using System.Configuration;
using log4net.Config;
using persistance.database_repository;
using persistance.repositoryInterfaces;
using Persistance.service;
using persistance.validators;
using server.servers;
using servicies;

namespace server;

public class Server
{
    public static void Main(string[] arg)
    {
        String fileName = "D:\\projectsRiderFinal\\clientserver\\clientserver\\log4net.xml";
        XmlConfigurator.Configure(new FileInfo(fileName));
        IDictionary<string, string> dbProperties = new SortedList<string, string>();
        dbProperties.Add("connectionString", GetConnectionStringByName("postgresDb"));
        DbUtils dbUtils = new DbUtils(dbProperties);
        ICharityRepository charityRepository = new CharityDbRepository(dbUtils);

        IDonorRepository donorRepository = new DonorDbRepository(dbUtils, new DonorValidator());
        IDonationRepository donationRepository = new DonationDbRepository(dbUtils, new DonationValidator());

        IVolunteerRepository volunteerRepository = new VolunteerDbRepository(dbUtils);
        IService teledonService = new TeledonService(charityRepository, donationRepository, donorRepository,
            volunteerRepository);

        AbstractServer server = new ConcurrentServer("127.0.0.1", 10102, teledonService);
        server.StartServer();
    }

    static string GetConnectionStringByName(string name)
    {
        // Assume failure.
        string returnValue = null;

        // Look for the name in the connectionStrings section.
        ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

        // If found, return the connection string.
        if (settings != null)
            returnValue = settings.ConnectionString;

        return returnValue;
    }
}