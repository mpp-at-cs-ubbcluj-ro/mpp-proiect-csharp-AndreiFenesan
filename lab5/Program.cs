
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using lab5.repositories.db_implementations;
using lab5.repositories.interfaces;
using lab5.servicies;
using log4net.Config;
using Teledon.validators;

namespace lab5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String fileName = "D\\lab5Gui\\lab5\\lab5\\Properties\\log4net.xml";
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form1 = new Form1(teledonSerice);
            form1.Show();
            Application.Run();
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