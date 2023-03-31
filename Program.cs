// See https://aka.ms/new-console-template for more informatio

using System.Configuration;
using log4net;
using log4net.Config;
using Teledon.models;
using Teledon.repositories.db_implementations;
using Teledon.repositories.interfaces;
using System.Security.Cryptography;
using System.Text;
using Teledon.servicies;
using Teledon.validators;

namespace Teledon
{
    class MainClass
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MainClass));

        public static void Main()
        {
            String fileName = "../../../properties/log4net.xml";
            XmlConfigurator.Configure(new FileInfo(fileName));
            IDictionary<string, string> dbProperties = new SortedList<string, string>();
            dbProperties.Add("connectionString", GetConnectionStringByName("postgresDb"));
            DbUtils dbUtils = new DbUtils(dbProperties);
            ICharityRepository charityRepository = new CharityDbRepository(dbUtils);

            IDonorRepository donorRepository = new DonorDbRepository(dbUtils, new DonorValidator());
            IDonationRepository donationRepository = new DonationDbRepository(dbUtils, new DonationValidator());

            IVolunteerRepository volunteerRepository = new VolunteerDbRepository(dbUtils);
            IService teledonSerice = new TeledonService(charityRepository, donationRepository, donorRepository,
                volunteerRepository);
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
}