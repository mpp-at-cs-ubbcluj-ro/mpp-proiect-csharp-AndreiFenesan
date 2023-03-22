using Teledon.models;

namespace Teledon.repositories.interfaces;

public interface IVolunteerRepository : IRepository<long, Volunteer>
{
    Volunteer? FindVolunteerByUsername(string username);
}