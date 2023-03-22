// See https://aka.ms/new-console-template for more informatio

using System.Configuration;
using log4net;
using log4net.Config;
using Teledon.models;
using Teledon.repositories.db_implementations;
using Teledon.repositories.interfaces;

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
            List<CharityCase> charityCases = charityRepository.FindAll();
            foreach (var charityCase in charityCases)
            {
                Console.WriteLine(charityCase);
            }

            IDonorRepository donorRepository = new DonorDbRepository(dbUtils);
            // Console.WriteLine(donorRepository.Save(new Donor("Marius Ielcean","MariusIel@yahoo.com","0741778823")));
            // IEnumerator<Donor> donors = donorRepository.FindDonorByNameLike("%nicu%");
            // while (donors.MoveNext())
            // {
            //     Console.WriteLine(donors.Current);
            // }
            // Console.WriteLine(donorRepository.FindDonorByPhoneNumber("0741733889"));
            // Console.WriteLine(donorRepository.findOneById(20));
            IDonationRepository donationRepository = new DonationDbRepository(dbUtils);
            // Donation don = donationRepository.Save(new Donation(200, 1, 1));
            // Console.WriteLine(don);
            // Console.WriteLine(donationRepository.GetTotalAmountOfMoneyRaised(1));
            IVolunteerRepository volunteerRepository = new VolunteerDbRepository(dbUtils);
            Console.WriteLine(volunteerRepository.FindVolunteerByUsername("serPop"));

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