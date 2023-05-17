using System.Security.Cryptography;
using System.Text;
using model;
using persistance.repositoryInterfaces;
using servicies;


namespace Persistance.service;

public class TeledonService : IService, IObservable
{
    private ICharityRepository _charityRepository;
    private IDonationRepository _donationRepository;
    private IDonorRepository _donorRepository;
    private IVolunteerRepository _volunteerRepository;
    private List<IObserver> _observers;

    public TeledonService(ICharityRepository charityRepository, IDonationRepository donationRepository,
        IDonorRepository donorRepository, IVolunteerRepository volunteerRepository)
    {
        _charityRepository = charityRepository;
        _donationRepository = donationRepository;
        _donorRepository = donorRepository;
        _volunteerRepository = volunteerRepository;
        _observers = new List<IObserver>();
    }

    public bool AuthenticateVolunteer(string username, string password)
    {
        Volunteer? volunteer = _volunteerRepository.FindVolunteerByUsername(username);
        if (volunteer == null)
        {
            return false;
        }

        String hashedPassword = ComputeSha256(password);
        return volunteer.Password == hashedPassword;
    }

    public bool AddNewDonation(long charityCaseId, string name, string emailAddress, string? phoneNumber,
        double donationAmount)
    {
        if (_charityRepository.findOneById(charityCaseId) == null)
        {
            throw new ServiceException("No charity case found with this id " + charityCaseId);
        }

        if (phoneNumber == null)
        {
            throw new ServiceException("Invalid phone number");
        }

        Donor donor = new Donor(name, emailAddress, phoneNumber);
        Donor? foundDonor = _donorRepository.FindDonorByPhoneNumber(phoneNumber);
        if (foundDonor == null)
        {
            //no donor found
            foundDonor = _donorRepository.Save(donor);
        }

        if (donor.Name != foundDonor.Name || donor.EmailAddress != foundDonor.EmailAddress ||
            foundDonor.PhoneNumber != donor.PhoneNumber)
        {
            throw new ServiceException("Invalid data for phone number " + phoneNumber);
        }

        donor.Id = foundDonor.Id;
        Donation donation = new Donation(donationAmount, donor.Id, charityCaseId);
        _donationRepository.Save(donation);
        this.NotifyAllObservers();
        return true;
    }


    public IEnumerator<Donor> GetDonorsWithNameContaining(string? containsInName)
    {
        if (String.IsNullOrWhiteSpace(containsInName))
        {
            throw new ServiceException("Invalid value to search for in donors names");
        }

        string pattern = "%" + containsInName + "%";
        return _donorRepository.FindDonorByNameLike(pattern);
    }

    public List<CharityCaseDto> GetAllCharityCases()
    {
        lock (this._charityRepository)
        {
            List<CharityCase> charityCases = _charityRepository.FindAll();
            List<CharityCaseDto> charityCaseDtos = new List<CharityCaseDto>();
            foreach (var charityCase in charityCases)
            {
                double amount = _donationRepository.GetTotalAmountOfMoneyRaised(charityCase.Id);
                CharityCaseDto charityCaseDto =
                    new CharityCaseDto(amount, charityCase.Id, charityCase.CaseName, charityCase.Description);
                charityCaseDtos.Add(charityCaseDto);
            }

            return charityCaseDtos;
        }
    }

    private String ComputeSha256(String str)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                stringBuilder.Append(bytes[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }

    public void AddObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyAllObservers()
    {
        foreach (var observer in _observers)
        {
            observer.UpdateTeledonEvent();
        }
    }
}