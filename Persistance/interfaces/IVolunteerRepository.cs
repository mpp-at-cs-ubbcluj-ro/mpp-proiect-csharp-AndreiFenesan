namespace Persistance.interfaces;
using model;
public interface IVolunteerRepository : IRepository<long, Volunteer>
{
    Volunteer? FindVolunteerByUsername(string username);
}