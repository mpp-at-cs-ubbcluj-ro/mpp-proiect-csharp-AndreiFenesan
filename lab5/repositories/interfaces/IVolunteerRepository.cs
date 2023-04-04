using lab5.models;

namespace lab5.repositories.interfaces;

public interface IVolunteerRepository : IRepository<long, Volunteer>
{
    Volunteer? FindVolunteerByUsername(string username);
}